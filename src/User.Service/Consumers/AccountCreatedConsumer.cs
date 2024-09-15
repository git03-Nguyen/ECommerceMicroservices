using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace User.Service.Consumers;

public class AccountCreatedConsumer : IConsumer<IAccountCreated>
{
    private readonly IMediator _mediator;

    public AccountCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountCreated> context)
    {
        var newAccountCreated = context.Message;
        // await _mediator.Send(new CreateUserCommand(newAccountCreated));
        Console.WriteLine($"Account created: {newAccountCreated.UserName} - {newAccountCreated.Role}");
    }
}