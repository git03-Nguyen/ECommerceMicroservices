using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;
using Order.Service.Features.Commands.BasketCommands.CheckoutBasket;

namespace Order.Service.Consumers;

public class CheckoutBasketConsumer : IConsumer<ICheckoutBasket>
{
    private readonly IMediator _mediator;

    public CheckoutBasketConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ICheckoutBasket> context)
    {
        var basket = context.Message;
        await _mediator.Send(new CheckoutBasketCommand(basket));
    }
}