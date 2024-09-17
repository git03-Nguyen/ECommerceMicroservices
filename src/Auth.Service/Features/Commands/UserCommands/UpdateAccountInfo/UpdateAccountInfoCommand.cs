using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoCommand : IRequest
{
    public UpdateAccountInfoCommand(IUserUpdated payload)
    {
        Payload = payload;
    }

    public IUserUpdated Payload { get; set; }
}