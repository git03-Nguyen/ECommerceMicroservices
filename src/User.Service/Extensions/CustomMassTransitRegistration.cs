using System.Reflection;
using Contracts.MassTransit.Extensions;
using MassTransit;
using User.Service.Consumers;

namespace User.Service.Extensions;

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

            cfg.ReceiveEndpoint("account-updated", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<AccountUpdatedConsumer>(context);
            });

            cfg.ReceiveEndpoint("account-deleted", e =>
            {
                e.UseMessageRetry(r => r.Immediate(5));
                e.AutoDelete = false;
                e.Durable = true;
                e.ConfigureConsumer<AccountDeletedConsumer>(context);
            });
        });

        return services;
    }
}