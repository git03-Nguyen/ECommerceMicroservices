using System.Reflection;
using Catalog.Service.Consumers;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using Contracts.MassTransit.Messages.Events.Order;
using MassTransit;
using RabbitMQ.Client;

namespace Catalog.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var nameGenerator = new CustomKebabNameGenerator();

            // Sending: ICreateProduct -> send-product-created [DIRECT]
            var createProductExchange = nameGenerator.SantinizeSendingExchangeName(nameof(ICreateProduct));
            cfg.Message<ICreateProduct>(e => e.SetEntityName(createProductExchange));
            cfg.Publish<ICreateProduct>(e => e.ExchangeType = ExchangeType.Direct);
            cfg.Send<ICreateProduct>(e => { });

            // Sending: IDeleteProducts -> send-product-deleted [DIRECT]
            var deleteProductExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IDeleteProducts));
            cfg.Message<IDeleteProducts>(e => e.SetEntityName(deleteProductExchange));
            cfg.Publish<IDeleteProducts>(e => e.ExchangeType = ExchangeType.Direct);
            cfg.Send<IDeleteProducts>(e => { });

            // Sending: IProductUdated -> send-product-updated [DIRECT]
            var updateProductExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IProductUpdated));
            cfg.Message<IProductUpdated>(e => e.SetEntityName(updateProductExchange));
            cfg.Publish<IProductUpdated>(e => e.ExchangeType = ExchangeType.Direct);
            cfg.Send<IProductUpdated>(e => { });

            // Registering: IAccountCreated -> account-created from send-account-created [seller.created]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerCreated) + "-in_catalog"), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                re.Bind(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerCreated)));

                re.ConfigureConsumer<SellerCreatedConsumer>(context);
            });

            // Registering: IAccountDeleted -> account-deleted from send-account-deleted [seller.deleted]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(ISellerDeleted) + "-in_catalog"), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                re.Bind(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerDeleted)));

                re.ConfigureConsumer<SellerDeletedConsumer>(context);
            });

            // Registering IUserUpdated -> user-info-updated from send-user-info-updated [seller.updated]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName("ISellerUpdated") + "-in_catalog", re =>
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

                re.ConfigureConsumer<SellerInfoUpdatedConsumer>(context);
            });

            // Registering IOrderCreated -> order-created from send-order-created [TOPIC]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IOrderCreated) + "-in_catalog"), re =>
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