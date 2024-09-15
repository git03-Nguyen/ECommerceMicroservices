using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;

public class UpdateSellerInfoCommand : IRequest
{
    public UpdateSellerInfoCommand(IUserUpdated payload)
    {
        Payload = payload;
    }

    public IUserUpdated Payload { get; set; }
}