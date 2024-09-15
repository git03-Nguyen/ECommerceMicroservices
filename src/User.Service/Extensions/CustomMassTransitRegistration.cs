using System.Reflection;
using Contracts.Helpers;
using Contracts.MassTransit.Extensions;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MassTransit;
using RabbitMQ.Client;
using User.Service.Consumers;

namespace User.Service.Extensions;

public static class CustomMassTransitRegistration
{
    public static IServiceCollection AddCustomMassTransitRegistration(this IServiceCollection services,
        IConfiguration configuration, Assembly? entryAssembly)
    {
        services.AddMassTransitRegistration(configuration, entryAssembly, (context, cfg) =>
        {
            var nameGenerator = new CustomKebabNameGenerator();

            // Sending: IUserUpdated -> send-user-info-updated
            var userInfoUpdatedExchange = nameGenerator.SantinizeSendingExchangeName(nameof(IUserUpdated));
            cfg.Message<IUserUpdated>(e => e.SetEntityName(userInfoUpdatedExchange));
            cfg.Publish<IUserUpdated>(e => e.ExchangeType = ExchangeType.Topic);
            cfg.Send<IUserUpdated>(e =>
            {
                e.UseRoutingKeyFormatter(ctx =>
                {
                    var role = ctx.Message.Role.ToLower();
                    return $"{role}.updated";
                });
            });

            // Registering: IAccountCreated -> account-created from send-account-created ~ *.created
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IAccountCreated)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                // re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountCreated));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "*.created";
                    e.ExchangeType = ExchangeType.Topic;
                });

                re.ConfigureConsumer<AccountCreatedConsumer>(context);
            });

            // Registering: IAccountDeleted -> account-deleted from send-account-deleted ~ *.deleted
            cfg.ReceiveEndpoint(nameGenerator.SantinizeReceivingQueueName(nameof(IAccountDeleted)), re =>
            {
                re.ConfigureConsumeTopology = false;
                re.SetQuorumQueue();
                // re.SetQueueArgument("declare", "lazy");
                re.UseMessageRetry(r =>
                    r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5)));
                re.AutoDelete = false;
                re.Durable = true;

                var exchangeName = nameGenerator.SantinizeSendingExchangeName(nameof(IAccountDeleted));
                re.Bind(exchangeName, e =>
                {
                    e.RoutingKey = "*.deleted";
                    e.ExchangeType = ExchangeType.Topic;
                });

                re.ConfigureConsumer<AccountDeletedConsumer>(context);
            });
        });

        return services;
    }
}