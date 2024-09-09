using Basket.Service.Features.Commands.BasketCommands.DeleteBasket;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class AccountDeletedConsumer : IConsumer<AccountDeleted>
{
    private readonly IMediator _mediator;

    public AccountDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountDeleted> context)
    {
        var message = context.Message;
        if (message.Role != ApplicationRoleConstants.Customer) return;
        await _mediator.Send(new DeleteBasketCommand(message));
    }
}