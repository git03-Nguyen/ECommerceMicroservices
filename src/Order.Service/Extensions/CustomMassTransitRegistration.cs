using System.Reflection;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using Order.Service.Consumers;

namespace Order.Service.Extensions;

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
                const string catalogQueue = "order";

                var checkOutBasketQueue = kebabFormatter.SanitizeName(nameof(CheckoutBasket));
                cfg.ReceiveEndpoint($"{checkOutBasketQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<CheckoutBasketConsumer>(context);
                });
                
            });
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        services.AddScoped<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();

        return services;
    }
}