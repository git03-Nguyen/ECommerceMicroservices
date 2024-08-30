using System.Reflection;
using Contracts.Masstransit.Extensions;
using Contracts.Masstransit.Queues;
using MassTransit;
using Order.Service.Consumers;

namespace Order.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services, IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var kebabFormatter = new KebabCaseEndpointNameFormatter(false);
            cfg.ReceiveEndpoint(kebabFormatter.SanitizeName(nameof(CheckoutBasket)), e =>
            {
                e.ConfigureConsumer<CheckoutBasketConsumer>(context);
            });
        });

        return services;
    }

}