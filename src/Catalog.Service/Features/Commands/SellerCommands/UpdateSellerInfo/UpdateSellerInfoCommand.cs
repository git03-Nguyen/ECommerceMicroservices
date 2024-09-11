using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;

public class UpdateSellerInfoCommand : IRequest
{
    public UserInfoUpdated Payload { get; set; }
    
    public UpdateSellerInfoCommand(UserInfoUpdated payload)
    {
        Payload = payload;
    }
}