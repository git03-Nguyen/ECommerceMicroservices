using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductsStock;

public class UpdateProductsStockCommand : IRequest
{
    public IOrderCreated Payload { get; set; }
    
    public UpdateProductsStockCommand(IOrderCreated payload)
    {
        Payload = payload;
    }
    
}