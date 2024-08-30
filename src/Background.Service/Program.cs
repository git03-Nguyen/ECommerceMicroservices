using Contracts.Masstransit.Core;
using Contracts.Masstransit.Extensions;
using MassTransit;

namespace Background.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ConfigureEndpoints(context);

            });
        });
        
        builder.Services.AddSingleton<ISendEndpointCustomProvider, SendEndpointCustomProvider>();
        
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}