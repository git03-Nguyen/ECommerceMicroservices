using System.Reflection;
using Contracts.MassTransit.Extensions;
using MassTransit;

namespace Catalog.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var kebabFormatter = new KebabCaseEndpointNameFormatter(false);
        });

        return services;
    }
}