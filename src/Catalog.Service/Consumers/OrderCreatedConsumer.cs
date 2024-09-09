using Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;
using Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;
using Contracts.MassTransit.Messages.Commands;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly IMediator _mediator;

    public OrderCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var message = context.Message;
        var products = await _mediator.Send(new UpdateStockAfterOrderCreatedCommand(message));
        // TODO: Send ProductPriceStockUpdated event to Basket (must get response from UpdateStockAfterOrderCreatedCommand)
        foreach (var product in products.Payload)
        {
            var request = new ProductPriceStockUpdated(product.ProductId, product.Price, product.Stock);
            var queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(nameof(ProductPriceStockUpdated));
            if (!string.IsNullOrWhiteSpace(queueName))
            {
                var sendEndpoint = await context.GetSendEndpoint(new Uri($"queue:{queueName}"));
                await sendEndpoint.Send<ProductPriceStockUpdated>(request);
            }
        }
        
    }
}