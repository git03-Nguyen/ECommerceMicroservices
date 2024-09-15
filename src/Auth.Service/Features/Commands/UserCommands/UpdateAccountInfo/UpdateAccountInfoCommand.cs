using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoCommand : IRequest
{
    public IUserInfoUpdated Payload { get; set; }

    public UpdateAccountInfoCommand(IUserInfoUpdated payload)
    {
        Payload = payload;
    }
    
}