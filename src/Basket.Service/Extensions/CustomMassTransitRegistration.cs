using System.Reflection;
using Basket.Service.Consumers;
using Contracts.MassTransit.Extensions;
using MassTransit;

namespace Basket.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var kebabFormatter = new KebabCaseEndpointNameFormatter(false);

            cfg.ReceiveEndpoint("new-account-created", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<NewAccountCreatedConsumer>(context);
            });

            cfg.ReceiveEndpoint("product-info-updated", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<ProductInfoUpdatedConsumer>(context);
            });

            cfg.ReceiveEndpoint("product-price-stock-updated", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<ProductPriceStockUpdatedConsumer>(context);
            });

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