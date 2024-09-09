using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoCommand : IRequest
{
    public UserUpdated Payload { get; set; }

    public UpdateAccountInfoCommand(UserUpdated payload)
    {
        Payload = payload;
    }
    
}