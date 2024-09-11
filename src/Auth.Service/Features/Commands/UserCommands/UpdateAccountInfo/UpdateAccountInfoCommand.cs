using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoCommand : IRequest
{
    public UserInfoUpdated Payload { get; set; }

    public UpdateAccountInfoCommand(UserInfoUpdated payload)
    {
        Payload = payload;
    }
    
}