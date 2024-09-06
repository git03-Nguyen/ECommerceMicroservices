using System.Reflection;
using Contracts.MassTransit.Extensions;
using MassTransit;
using Order.Service.Consumers;

namespace Order.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            // var queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(CheckoutBasket));

            cfg.ReceiveEndpoint("checkout-basket", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<CheckoutBasketConsumer>(context);
            });
        });


        return services;
    }
}