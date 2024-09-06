using Contracts.MassTransit.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedCommand : IRequest
{
    public UpdateStockAfterOrderCreatedCommand(OrderCreated payload)
    {
        Payload = payload;
    }

    public OrderCreated Payload { get; set; }
}