using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using Order.Service.Features.Commands.BasketCommands.CheckoutBasket;

namespace Order.Service.Consumers;

public class CheckoutBasketConsumer : IConsumer<CheckoutBasket>
{
    private readonly IMediator _mediator;

    public CheckoutBasketConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CheckoutBasket> context)
    {
        var basket = context.Message;
        await _mediator.Send(new CheckoutBasketCommand(basket));
    }
}