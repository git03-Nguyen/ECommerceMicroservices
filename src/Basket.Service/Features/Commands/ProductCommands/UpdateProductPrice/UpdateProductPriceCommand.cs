using Contracts.MassTransit.Queues;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductPrice;

public class UpdateProductPriceCommand : IRequest
{
    public UpdateProductPriceCommand(ProductPriceStockUpdated payload)
    {
        Payload = payload;
    }

    public ProductPriceStockUpdated Payload { get; }
}