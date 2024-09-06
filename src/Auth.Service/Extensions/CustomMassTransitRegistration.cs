using System.Reflection;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEnpoint;
using MassTransit;

namespace Auth.Service.Extensions;

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

                // cfg.ReceiveEndpoint("new-account-created_error",
                //     e => { e.Consumer<NewAccountCreatedFaultConsumer>(context); });
            });
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        services.AddScoped<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();

        return services;
    }
}