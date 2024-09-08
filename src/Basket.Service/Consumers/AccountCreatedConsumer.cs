using Basket.Service.Features.Commands.BasketCommands.CreateBasket;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class AccountCreatedConsumer : IConsumer<AccountCreated>
{
    private readonly IMediator _mediator;

    public AccountCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountCreated> context)
    {
        var message = context.Message;

        if (message.Role == ApplicationRoleConstants.Customer)
        {
            await _mediator.Send(new CreateBasketCommand(message));
        }
    }
}