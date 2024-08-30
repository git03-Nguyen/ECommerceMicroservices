using Contracts.Masstransit.Queues;
using MassTransit;
using Newtonsoft.Json;

namespace Order.Service.Consumers;

public class CountTimeConsumer : IConsumer<CheckoutBasket>
{
    private readonly ILogger<CountTimeConsumer> _logger;
    
    public CountTimeConsumer(ILogger<CountTimeConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckoutBasket> context)
    {
        var currentTime = context.Message;
        _logger.LogInformation($"Count: {currentTime.BasketId}");
        
    }
}