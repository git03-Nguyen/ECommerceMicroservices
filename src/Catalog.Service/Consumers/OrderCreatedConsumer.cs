using Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly IMediator _mediator;

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        await _mediator.Send(new UpdateStockAfterOrderCreatedCommand(context.Message));
    }
}