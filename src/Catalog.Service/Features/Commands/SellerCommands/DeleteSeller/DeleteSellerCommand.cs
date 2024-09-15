using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerCommand : IRequest
{
    public DeleteSellerCommand(IAccountDeleted payload)
    {
        Payload = payload;
    }

    public IAccountDeleted Payload { get; }
}