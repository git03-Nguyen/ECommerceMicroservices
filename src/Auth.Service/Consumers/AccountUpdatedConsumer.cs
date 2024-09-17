using Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MassTransit;
using MediatR;

namespace Auth.Service.Consumers;

public class AccountUpdatedConsumer : IConsumer<IUserUpdated>
{
    private readonly IMediator _mediator;

    public AccountUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateAccountInfoCommand(message));
    }
}