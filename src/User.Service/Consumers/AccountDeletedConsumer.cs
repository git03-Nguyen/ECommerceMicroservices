using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using User.Service.Features.Commands.UserCommands.DeleteUser;

namespace User.Service.Consumers;

public class AccountDeletedConsumer : IConsumer<IAccountDeleted>
{
    private readonly IMediator _mediator;

    public AccountDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountDeleted> context)
    {
        var accountDeleted = context.Message;
        await _mediator.Send(new DeleteUserCommand(accountDeleted.AccountId));
    }
}