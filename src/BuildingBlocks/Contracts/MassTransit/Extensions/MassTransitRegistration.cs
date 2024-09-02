using System.Reflection;
using Contracts.MassTransit.Core.SendEnpoint;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts.MassTransit.Extensions;

public static class MassTransitRegistration
{
    public static IServiceCollection AddMassTransitRegistration(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? entryAssembly = null,
        Action<IBusRegistrationContext, IBusFactoryConfigurator>? registrationConfigure = null)
    {
        var rabbitMqHostName = configuration.GetSection("RabbitMq:HostName").Value;
        var rabbitMqUserName = configuration.GetSection("RabbitMq:UserName").Value;
        var rabbitMqPassword = configuration.GetSection("RabbitMq:Password").Value;
        if (string.IsNullOrWhiteSpace(rabbitMqHostName) || string.IsNullOrWhiteSpace(rabbitMqUserName) ||
            string.IsNullOrWhiteSpace(rabbitMqPassword))
            throw new ArgumentNullException("RabbitMq configuration is invalid");
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

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();

        return services;
    }
}