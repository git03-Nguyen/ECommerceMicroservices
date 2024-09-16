using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerCommand : IRequest
{
    public IUserUpdated Payload { get; }
    
    public UpdateSellerCommand(IUserUpdated payload)
    {
        Payload = payload;
    }
    
}