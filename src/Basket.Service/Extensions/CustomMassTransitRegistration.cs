using System.Reflection;
using Basket.Service.Consumers;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Extensions;
using MassTransit;

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

                cfg.ReceiveEndpoint("new-account-created_basket", e =>
                {
                    e.UseMessageRetry(r => r.Immediate(5));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<NewAccountCreatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("product-info-updated_basket", e =>
                {
                    e.UseMessageRetry(r => r.Immediate(5));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<ProductInfoUpdatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("product-price-stock-updated_basket", e =>
                {
                    e.UseMessageRetry(r => r.Immediate(5));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<ProductPriceStockUpdatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("order-created_basket", e =>
                {
                    e.UseMessageRetry(r => r.Immediate(5));
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