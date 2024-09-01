using Contracts.Domain;
using Contracts.MassTransit.Queues;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using Order.Service.Data.Models;

namespace Order.Service.Consumers;

public class CheckoutBasketConsumer : IConsumer<CheckoutBasket>
{
    private readonly ILogger<CheckoutBasketConsumer> _logger;
    private readonly IMediator _mediator;

    public CheckoutBasketConsumer(ILogger<CheckoutBasketConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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