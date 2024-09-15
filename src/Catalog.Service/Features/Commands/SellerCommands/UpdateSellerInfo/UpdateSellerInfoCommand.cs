using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;

public class UpdateSellerInfoCommand : IRequest
{
    public IUserInfoUpdated Payload { get; set; }
    
    public UpdateSellerInfoCommand(IUserInfoUpdated payload)
    {
        Payload = payload;
    }
}