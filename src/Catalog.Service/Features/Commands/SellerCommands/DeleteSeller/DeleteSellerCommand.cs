using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerCommand : IRequest
{
    public IAccountDeleted Payload { get; }
    
    public DeleteSellerCommand(IAccountDeleted payload)
    {
        Payload = payload;
    }

}