using System.Reflection;
using Auth.Service.Consumers;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Core.SendEnpoint;
using Contracts.MassTransit.Extensions;
using MassTransit;

namespace Auth.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransit(x =>
        {
            if (entryAssembly is not null) x.AddConsumers(entryAssembly);
            // x.AddRequestClient();
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host($"rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("new-account-created_error", e =>
                {
                    e.Consumer<NewAccountCreatedFaultConsumer>(context);
                });
            });
        });

        services.AddTransient<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        services.AddTransient<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();

        return services;
    }
}