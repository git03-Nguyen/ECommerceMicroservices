using Basket.Service.Features.Commands.BasketCommands.CreateBasket;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class CustomerCreatedConsumer : IConsumer<IAccountCreated>
{
    private readonly IMediator _mediator;

    public CustomerCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountCreated> context)
    {
        var message = context.Message;
        await _mediator.Send(new CreateBasketCommand(message));
    }
}