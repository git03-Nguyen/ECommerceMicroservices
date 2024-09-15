using System.Reflection;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using Order.Service.Consumers;
using RabbitMQ.Client;

namespace Order.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var nameGenerator = new CustomKebabNameGenerator();

            // Send IOrderCreated -> send-order-created [TOPIC]
            var orderCreatedExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IOrderCreated));
            cfg.Message<IOrderCreated>(e => e.SetEntityName(orderCreatedExchange));
            cfg.Publish<IOrderCreated>(e => e.ExchangeType = ExchangeType.Topic);
            cfg.Send<IOrderCreated>(e => { e.UseRoutingKeyFormatter(ctx => ""); });

            // ReceiveEndpoint for ICheckoutBasket -> checkout-basket from send-checkout-basket [DIRECT]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ICheckoutBasket)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(ICheckoutBasket));
                re.Bind(exchangeName, e => { e.ExchangeType = ExchangeType.Direct; });

                re.ConfigureConsumer<CheckoutBasketConsumer>(context);
            });
        });

        return services;
    }
}