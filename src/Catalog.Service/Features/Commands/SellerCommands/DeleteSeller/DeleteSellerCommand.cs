using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerCommand : IRequest
{
    public AccountDeleted Payload { get; }
    
    public DeleteSellerCommand(AccountDeleted payload)
    {
        Payload = payload;
    }

}