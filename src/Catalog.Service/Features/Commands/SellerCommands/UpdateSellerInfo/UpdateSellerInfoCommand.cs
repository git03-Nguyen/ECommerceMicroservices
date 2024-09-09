using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;

public class UpdateSellerInfoCommand : IRequest
{
    public UserUpdated Payload { get; set; }
    
    public UpdateSellerInfoCommand(UserUpdated payload)
    {
        Payload = payload;
    }
}