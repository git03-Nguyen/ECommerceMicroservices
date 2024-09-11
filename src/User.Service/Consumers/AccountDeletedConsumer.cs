using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using User.Service.Features.Commands.UserCommands.DeleteUser;

namespace User.Service.Consumers;

public class AccountDeletedConsumer : IConsumer<AccountDeleted>
{
    private readonly IMediator _mediator;

    public AccountDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountDeleted> context)
    {
        var accountDeleted = context.Message;
        if (accountDeleted.Role == ApplicationRoleConstants.Customer)
            await _mediator.Send(new DeleteUserCommand(accountDeleted.AccountId));
        else if (accountDeleted.Role == ApplicationRoleConstants.Seller)
            await _mediator.Send(new DeleteUserCommand(accountDeleted.AccountId));
    }
}