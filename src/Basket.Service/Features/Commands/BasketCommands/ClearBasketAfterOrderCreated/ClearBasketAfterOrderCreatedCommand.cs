using Contracts.MassTransit.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.ClearBasketAfterOrderCreated;

public class ClearBasketAfterOrderCreatedCommand : IRequest
{
    public ClearBasketAfterOrderCreatedCommand(OrderCreated payload)
    {
        Payload = payload;
    }

    public OrderCreated Payload { get; set; }
}