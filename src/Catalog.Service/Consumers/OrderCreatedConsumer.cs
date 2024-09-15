using Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Order;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class OrderCreatedConsumer : IConsumer<IOrderCreated>
{
    private readonly IMediator _mediator;

    public OrderCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IOrderCreated> context)
    {
        var message = context.Message; 
        await _mediator.Send(new UpdateStockAfterOrderCreatedCommand(message));
    }
}