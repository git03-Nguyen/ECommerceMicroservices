using Contracts.MassTransit.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.RollbackSignUp;

public class RollBackSignUpCommand : IRequest
{
    public NewAccountCreated Payload { get; set; }
    
    public RollBackSignUpCommand(NewAccountCreated payload)
    {
        Payload = payload;
    }
    
}