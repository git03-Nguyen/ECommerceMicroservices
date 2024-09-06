using Contracts.MassTransit.Queues;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductPriceStock;

public class UpdateProductPriceStockCommand : IRequest
{
    public UpdateProductPriceStockCommand(ProductPriceStockUpdated payload)
    {
        Payload = payload;
    }

    public ProductPriceStockUpdated Payload { get; }
}