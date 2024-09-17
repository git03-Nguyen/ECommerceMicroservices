using Contracts.MassTransit.Messages.Events.Order;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductsStock;

public class UpdateProductsStockCommand : IRequest
{
    public UpdateProductsStockCommand(IOrderCreated payload)
    {
        Payload = payload;
    }

    public IOrderCreated Payload { get; set; }
}