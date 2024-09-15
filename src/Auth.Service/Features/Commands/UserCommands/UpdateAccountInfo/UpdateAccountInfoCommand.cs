using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoCommand : IRequest
{
    public UpdateAccountInfoCommand(IUserInfoUpdated payload)
    {
        Payload = payload;
    }

    public IUserInfoUpdated Payload { get; set; }
}