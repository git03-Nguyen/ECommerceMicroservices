using Contracts.MassTransit.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.RollbackSignUp;

public class RollBackSignUpCommand : IRequest
{
    public RollBackSignUpCommand(NewAccountCreated payload)
    {
        Payload = payload;
    }

    public NewAccountCreated Payload { get; set; }
}