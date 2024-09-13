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
        Console.WriteLine("AccountDeletedConsumer:", context.Message);
        var accountDeleted = context.Message;
        await _mediator.Send(new DeleteUserCommand(accountDeleted.AccountId));
        // if (accountDeleted.Role == ApplicationRoleConstants.Customer)
        // else if (accountDeleted.Role == ApplicationRoleConstants.Seller)
        //     await _mediator.Send(new DeleteUserCommand(accountDeleted.AccountId));
    }
}