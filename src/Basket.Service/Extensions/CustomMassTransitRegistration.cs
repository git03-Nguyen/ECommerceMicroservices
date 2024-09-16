using System.Reflection;
using Basket.Service.Consumers;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MassTransit;
using RabbitMQ.Client;

namespace Basket.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var nameGenerator = new CustomKebabNameGenerator();

            // Sending: CheckoutBasket -> send-checkout-basket [DIRECT]
            var checkoutBasketExchange = nameGenerator.SantinizeSendingExchangeName(nameof(ICheckoutBasket));
            cfg.Message<ICheckoutBasket>(e => e.SetEntityName(checkoutBasketExchange));
            cfg.Publish<ICheckoutBasket>(e => e.ExchangeType = ExchangeType.Direct);
            cfg.Send<ICheckoutBasket>(e => { });

            // Registering: IAccountCreated -> recv-customer-created from send-account-created [TOPIC:customer.created]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IAccountCreated)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountCreated));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "customer.created";
                    e.ExchangeType = ExchangeType.Topic;
                });

                re.ConfigureConsumer<CustomerCreatedConsumer>(context);
            });

            // Registering: IAccountDeleted -> recv-customer-deleted from send-account-deleted [TOPIC:customer.deleted]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ICustomerDeleted)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountDeleted));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "customer.deleted";
                    e.ExchangeType = ExchangeType.Topic;
                });

                re.ConfigureConsumer<CustomerDeletedConsumer>(context);
            });

            // Registering: IAccountCreated -> seller-created from send-account-created [TOPIC:seller.created]
            // cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerCreated)), re =>
            // {
            //     re.ConfigureConsumeTopology = false;
            //     re.SetQuorumQueue();
            //     re.SetQueueArgument("declare", "lazy");
            //     re.UseMessageRetry(r =>
            //         r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
            //     re.AutoDelete = false;
            //     re.Durable = true;
            //
            //     var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountCreated));
            //     re.Bind(exchangeName, e =>
            //     {
            //         e.RoutingKey = "seller.created";
            //         e.ExchangeType = ExchangeType.Topic;
            //     });
            //
            //     re.ConfigureConsumer<SellerCreatedConsumer>(context);
            // });

            // Registering: IAccountDeleted -> account-deleted from send-account-deleted [TOPIC:seller.deleted]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerDeleted)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountDeleted));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "seller.deleted";
                    e.ExchangeType = ExchangeType.Topic;
                });

                re.ConfigureConsumer<SellerDeletedConsumer>(context);
            });

            // Registering: IProductUpdated -> product-updated from send-product-updated [DIRECT]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IProductUpdated)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IProductUpdated));
                re.Bind(exchangeName, e => { e.ExchangeType = ExchangeType.Direct; });

                re.ConfigureConsumer<ProductUpdatedConsumer>(context);
            });

            // Registering: IProductsDeleted -> products-deleted from send-products-deleted [DIRECT]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IDeleteProducts)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IDeleteProducts));
                re.Bind(exchangeName, e => { e.ExchangeType = ExchangeType.Direct; });

                re.ConfigureConsumer<DeleteProductsConsumer>(context);
            });
        });

        return services;
    }
}