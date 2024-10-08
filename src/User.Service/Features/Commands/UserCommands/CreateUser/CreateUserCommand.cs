using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MediatR;

namespace User.Service.Features.Commands.UserCommands.CreateUser;

public class CreateUserCommand : IRequest<bool>
{
    public CreateUserCommand(IAccountCreated payload)
    {
        Payload = payload;
    }

    public IAccountCreated Payload { get; set; }
}