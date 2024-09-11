using System.Reflection;
using Basket.Service.Consumers;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using RabbitMQ.Client;

namespace Basket.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        var rabbitMqHostName = configuration.GetSection("RabbitMq:HostName").Value ?? "localhost";
        var rabbitMqUserName = configuration.GetSection("RabbitMq:UserName").Value ?? "guest";
        var rabbitMqPassword = configuration.GetSection("RabbitMq:Password").Value ?? "guest";
        
        services.AddMassTransit(x =>
        {
            if (entryAssembly is not null) x.AddConsumers(entryAssembly);
            // x.AddRequestClient();
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host($"rabbitmq://{rabbitMqHostName}", h =>
                {
                    h.Username(rabbitMqUserName);
                    h.Password(rabbitMqPassword);
                });

                var kebabFormatter = new KebabCaseEndpointNameFormatter(false);
                const string basketQueue = "basket";
                
                var accountCreatedQueue = kebabFormatter.SanitizeName(nameof(AccountCreated));
                cfg.ReceiveEndpoint($"{accountCreatedQueue}_{basketQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<AccountCreatedConsumer>(context);
                });
                
                var accountDeletedQueue = kebabFormatter.SanitizeName(nameof(AccountDeleted));
                cfg.ReceiveEndpoint($"{accountDeletedQueue}_{basketQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<AccountDeletedConsumer>(context);
                });

                var productInfoUpdatedQueue = kebabFormatter.SanitizeName(nameof(ProductInfoUpdated));
                cfg.ReceiveEndpoint($"{productInfoUpdatedQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<ProductInfoUpdatedConsumer>(context);
                });

                var productPriceStockUpdatedQueue = kebabFormatter.SanitizeName(nameof(ProductPriceStockUpdated));
                cfg.ReceiveEndpoint($"{productPriceStockUpdatedQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<ProductPriceStockUpdatedConsumer>(context);
                });
                
                var deleteProductQueue = kebabFormatter.SanitizeName(nameof(DeleteProducts));
                cfg.ReceiveEndpoint($"{deleteProductQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<DeleteProductConsumer>(context);
                });

                var orderCreatedQueue = kebabFormatter.SanitizeName(nameof(OrderCreated));
                cfg.ReceiveEndpoint($"{orderCreatedQueue}_{basketQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<OrderCreatedConsumer>(context);
                });
            });
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        services.AddScoped<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();

        return services;
    }
}