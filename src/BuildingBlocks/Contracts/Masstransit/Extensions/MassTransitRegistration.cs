using System.Reflection;
using Contracts.Masstransit.Core;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts.Masstransit.Extensions;

public static class MassTransitRegistration
{
    public static IServiceCollection AddMassTransitRegistration(
            this IServiceCollection services, 
            IConfiguration configuration, 
            Assembly? entryAssembly = null,
            Action<IBusRegistrationContext, IBusFactoryConfigurator>? registrationConfigure = null)
    {
        services.AddMassTransit(x =>
        {
            if (entryAssembly is not null)
            {
                x.AddConsumers(entryAssembly);
            }
            
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ConfigureEndpoints(context);

            });
        });
        
        services.AddScoped<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        
        return services;
    }

}