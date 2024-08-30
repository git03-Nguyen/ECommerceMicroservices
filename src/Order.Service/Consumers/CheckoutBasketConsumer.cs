using Contracts.Domain;
using Contracts.Masstransit.Queues;
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
        var basket = context.Message;
        _logger.LogInformation($"CheckoutBasketConsumer => {JsonConvert.SerializeObject(basket)}");
        
    }
}