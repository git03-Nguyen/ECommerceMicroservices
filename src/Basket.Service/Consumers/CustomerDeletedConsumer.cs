using Basket.Service.Features.Commands.BasketCommands.DeleteBasket;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class CustomerDeletedConsumer : IConsumer<IAccountDeleted>
{
    private readonly IMediator _mediator;

    public CustomerDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountDeleted> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteBasketCommand(message));
    }
}