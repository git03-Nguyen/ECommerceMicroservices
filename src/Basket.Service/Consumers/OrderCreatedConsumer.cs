using Basket.Service.Features.Commands.BasketCommands.ClearBasketAfterOrderCreated;
using Basket.Service.Features.Commands.BasketCommands.UpdateStockAfterOrderCreated;
using Contracts.MassTransit.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly IMediator _mediator;

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        await _mediator.Send(new ClearBasketAfterOrderCreatedCommand(context.Message));
        await _mediator.Send(new UpdateStockAfterOrderCreatedCommand(context.Message));
    }
}