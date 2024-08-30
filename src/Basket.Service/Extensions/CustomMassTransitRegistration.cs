using System.Reflection;
using Contracts.Masstransit.Extensions;
using Contracts.Masstransit.Queues;
using MassTransit;

namespace Basket.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services, IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var kebabFormatter = new KebabCaseEndpointNameFormatter(false);
            
        });

        return services;
    }

}