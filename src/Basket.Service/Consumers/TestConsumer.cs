using Contracts.MassTransit.Queues;
using MassTransit;
using Newtonsoft.Json;

namespace Basket.Service.Consumers;

public class TestConsumer : IConsumer<CheckoutBasket>
{
    private readonly ILogger<TestConsumer> _logger;

    public TestConsumer(ILogger<TestConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckoutBasket> context)
    {
        // Receive the basket confirmation message
        var basket = context.Message;
        _logger.LogInformation($"CheckoutBasketConsumer => {JsonConvert.SerializeObject(basket)}");

        // Create the order;
        // TODO: in the future, we will use the mediator to create the order
        // and split the orders based on seller
        // But now, for the sake of simplicity, we will create only 1 order

        // Check the price and quantity of the products in the basket
    }
}