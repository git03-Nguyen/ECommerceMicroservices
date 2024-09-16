using System.Reflection;
using Basket.Service.Consumers;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using Contracts.MassTransit.Messages.Events.Order;
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
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ICustomerCreated)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(ICustomerCreated));
                re.Bind(exchangeName);

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

                re.Bind(nameGenerator.SantinizeSendingExchangeName(nameof(ICustomerDeleted)));

                re.ConfigureConsumer<CustomerDeletedConsumer>(context);
            });

            // Registering: IAccountCreated -> seller-created from send-account-created [TOPIC:seller.created]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerCreated) + "-in_basket"), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;
            
                re.Bind(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerCreated)));
            
                re.ConfigureConsumer<SellerCreatedConsumer>(context);
            });

            // Registering: IAccountDeleted -> account-deleted from send-account-deleted [TOPIC:seller.deleted]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerDeleted) + "-in_basket"), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                re.Bind(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerDeleted)));

                re.ConfigureConsumer<SellerDeletedConsumer>(context);
            });
            
            // Registering IUserUpdated -> user-info-updated from send-user-info-updated [seller.updated]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName("ISellerUpdated") + "-in_basket", re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IUserUpdated));
                re.Bind("send-seller-updated");

                re.ConfigureConsumer<SellerUpdatedConsumer>(context);
            });
            
            // Registering: ICreateProduct -> product-created from send-product-created [DIRECT]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ICreateProduct)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(ICreateProduct));
                re.Bind(exchangeName, e => { e.ExchangeType = ExchangeType.Direct; });

                re.ConfigureConsumer<CreateProductConsumer>(context);
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
            
            // Registering IOrderCreated -> order-created from send-order-created [TOPIC]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IOrderCreated)+"-in_basket"), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IOrderCreated));
                re.Bind(exchangeName, e =>
                {
                    e.ExchangeType = ExchangeType.Topic;
                    e.RoutingKey = "order.created";
                });

                cfg.ConfigureEndpoints(context);    
                re.ConfigureConsumer<OrderCreatedConsumer>(context);
            });

        });

        return services;
    }
}