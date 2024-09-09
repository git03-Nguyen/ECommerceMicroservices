using System.Reflection;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using RabbitMQ.Client;
using User.Service.Consumers;

namespace User.Service.Extensions;

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
                const string userQueue = "user";

                var accountCreatedQueue = kebabFormatter.SanitizeName(nameof(AccountCreated));
                cfg.ReceiveEndpoint($"{accountCreatedQueue}_{userQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    // e.ExchangeType = ExchangeType.Topic;
                    // e.Bind<AccountCreated>(x =>
                    // {
                    //     x.RoutingKey = "*.created";
                    // });
                    e.ConfigureConsumer<AccountCreatedConsumer>(context);
                });

                var accountDeletedQueue = kebabFormatter.SanitizeName(nameof(AccountDeleted));
                cfg.ReceiveEndpoint($"{accountDeletedQueue}_{userQueue}", e =>
                {
                    e.UseMessageRetry(r => r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.ConfigureConsumer<AccountDeletedConsumer>(context);
                });
            });
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        services.AddScoped<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();

        return services;
    }
}