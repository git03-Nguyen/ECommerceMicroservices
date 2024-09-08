using Basket.Service.Features.Commands.ProductCommands.UpdateProductPriceStock;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class ProductPriceStockUpdatedConsumer : IConsumer<ProductPriceStockUpdated>
{
    private readonly IMediator _mediator;

    public ProductPriceStockUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductPriceStockUpdated> context)
    {
        var productPriceStockUpdated = context.Message;
        await _mediator.Send(new UpdateProductPriceStockCommand(productPriceStockUpdated));
    }
}