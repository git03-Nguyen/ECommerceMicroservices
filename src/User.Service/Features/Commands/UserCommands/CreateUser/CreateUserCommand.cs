using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace User.Service.Features.Commands.UserCommands.CreateUser;

public class CreateUserCommand : IRequest<bool>
{
    public CreateUserCommand(AccountCreated payload)
    {
        Payload = payload;
    }

    public AccountCreated Payload { get; set; }
}