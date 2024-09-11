using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using User.Service.Features.Commands.UserCommands.CreateUser;

namespace User.Service.Consumers;

public class AccountCreatedConsumer : IConsumer<AccountCreated>
{
    private readonly IMediator _mediator;

    public AccountCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountCreated> context)
    {
        var newAccountCreated = context.Message;
        await _mediator.Send(new CreateUserCommand(newAccountCreated));
    }
}