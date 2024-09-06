using System.Reflection;
using Catalog.Service.Consumers;
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

            cfg.ReceiveEndpoint("order-created", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<OrderCreatedConsumer>(context);
            });
        });

        return services;
    }
}