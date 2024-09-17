using Contracts.MassTransit.Messages.Events.Order;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedCommand : IRequest<UpdateStockAfterOrderCreatedResponse>
{
    public UpdateStockAfterOrderCreatedCommand(IOrderCreated payload)
    {
        Payload = payload;
    }

    public IOrderCreated Payload { get; }
}