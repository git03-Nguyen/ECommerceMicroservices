using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerCommand : IRequest
{
    public UpdateSellerCommand(IUserUpdated payload)
    {
        Payload = payload;
    }

    public IUserUpdated Payload { get; }
}