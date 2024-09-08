using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedCommand : IRequest
{
    public UpdateStockAfterOrderCreatedCommand(OrderCreated payload)
    {
        Payload = payload;
    }

    public OrderCreated Payload { get; }
}