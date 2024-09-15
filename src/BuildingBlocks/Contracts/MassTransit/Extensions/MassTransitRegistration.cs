using System.Reflection;
using Contracts.MassTransit.Endpoints.SendEndpoint;
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
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? registrationConfigure = null)
    {
        services.AddOptions<RabbitMqTransportOptions>().BindConfiguration(nameof(RabbitMqTransportOptions));

        services.AddMassTransit(x =>
        {
            if (entryAssembly is not null) x.AddConsumers(entryAssembly);

            x.UsingRabbitMq(registrationConfigure ?? ((context, cfg) => { }));
        });

        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();

        return services;
    }
}