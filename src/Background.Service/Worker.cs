using Contracts.Masstransit.Core;
using Contracts.Masstransit.Queues;

namespace Background.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;

    public Worker(ILogger<Worker> logger, ISendEndpointCustomProvider sendEndpointCustomProvider)
    {
        _logger = logger;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var model = new CheckoutBasket
                {
                    // BasketId = the second
                    BasketId = DateTime.UtcNow.Second
                };
                await _sendEndpointCustomProvider.SendMessage<CheckoutBasket>(model, stoppingToken, "count-time");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}