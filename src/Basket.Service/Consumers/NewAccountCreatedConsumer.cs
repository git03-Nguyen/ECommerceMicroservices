using Basket.Service.Features.Commands.BasketCommands.CreateBasket;
using Contracts.Constants;
using Contracts.MassTransit.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class NewAccountCreatedConsumer : IConsumer<NewAccountCreated>
{
    private readonly IMediator _mediator;

    public NewAccountCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<NewAccountCreated> context)
    {
        var message = context.Message;

        if (message.Role == ApplicationRoleConstants.Customer) await _mediator.Send(new CreateBasketCommand(message));
    }
}