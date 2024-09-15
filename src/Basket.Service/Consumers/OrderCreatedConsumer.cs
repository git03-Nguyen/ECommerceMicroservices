using Basket.Service.Features.Commands.ProductCommands.UpdateProductsStock;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Order;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

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
        await _mediator.Send(new UpdateProductsStockCommand(message));
    }
}