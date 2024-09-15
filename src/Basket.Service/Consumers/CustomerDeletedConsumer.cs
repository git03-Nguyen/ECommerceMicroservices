using Basket.Service.Features.Commands.BasketCommands.DeleteBasket;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class CustomerDeletedConsumer : IConsumer<ICustomerDeleted>
{
    private readonly IMediator _mediator;

    public CustomerDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ICustomerDeleted> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteBasketCommand(message));
    }
}