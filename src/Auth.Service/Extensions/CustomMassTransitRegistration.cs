using System.Reflection;
using Auth.Service.Consumers;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MassTransit;
using RabbitMQ.Client;

namespace Auth.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var nameGenerator = new CustomKebabNameGenerator();

            // Sending: IAccountCreated -> send-account-created
            var accountCreatedExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountCreated));
            cfg.Message<IAccountCreated>(e => e.SetEntityName(accountCreatedExchange));
            cfg.Publish<IAccountCreated>(e => { e.ExchangeType = ExchangeType.Topic; });
            cfg.Send<IAccountCreated>(e =>
            {
                e.UseRoutingKeyFormatter(ctx =>
                {
                    var role = ctx.Message.Role.ToLower();
                    return $"{role}.created";
                });
            });
            cfg.ReceiveEndpoint(accountCreatedExchange, e =>
            {
                e.ConfigureConsumeTopology = false;
                e.BindQueue = false;
                e.ExchangeType = ExchangeType.Topic;
            });
            cfg.ReceiveEndpoint(nameGenerator.SantinizeSendingExchangeName(nameof(ICustomerCreated)), e =>
            {
                e.BindQueue = false;
                e.Bind(accountCreatedExchange, s =>
                {
                    s.ExchangeType = ExchangeType.Topic;
                    s.RoutingKey = "customer.created";
                });
            });
            cfg.ReceiveEndpoint(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerCreated)), e =>
            {
                e.BindQueue = false;
                e.Bind(accountCreatedExchange, s =>
                {
                    s.ExchangeType = ExchangeType.Topic;
                    s.RoutingKey = "seller.created";
                });
            });

            // Sending: IAccountDeleted -> send-account-deleted
            var accountDeletedExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountDeleted));
            cfg.Message<IAccountDeleted>(e => e.SetEntityName(accountDeletedExchange));
            cfg.Publish<IAccountDeleted>(e => e.ExchangeType = ExchangeType.Topic);
            cfg.Send<IAccountDeleted>(e =>
            {
                e.UseRoutingKeyFormatter(ctx =>
                {
                    var role = ctx.Message.Role.ToLower();
                    return $"{role}.deleted";
                });
            });
            cfg.ReceiveEndpoint(accountDeletedExchange, e =>
            {
                e.ConfigureConsumeTopology = false;
                e.BindQueue = false;
                e.ExchangeType = ExchangeType.Topic;
            });
            cfg.ReceiveEndpoint(nameGenerator.SantinizeSendingExchangeName(nameof(ICustomerDeleted)), e =>
            {
                e.BindQueue = false;
                e.Bind(accountDeletedExchange, s =>
                {
                    s.ExchangeType = ExchangeType.Topic;
                    s.RoutingKey = "customer.deleted";
                });
            });
            cfg.ReceiveEndpoint(nameGenerator.SantinizeSendingExchangeName(nameof(ISellerDeleted)), e =>
            {
                e.BindQueue = false;
                e.Bind(accountDeletedExchange, s =>
                {
                    s.ExchangeType = ExchangeType.Topic;
                    s.RoutingKey = "seller.deleted";
                });
            });

            // Registering: IUserUpdated -> user-info-updated from send-user-info-updated [user.updated]
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IUserUpdated)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IUserUpdated));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "*.updated";
                    e.ExchangeType = ExchangeType.Topic;
                });

                cfg.ConfigureEndpoints(context);
                re.ConfigureConsumer<AccountUpdatedConsumer>(context);
            });
        });

        return services;
    }
}