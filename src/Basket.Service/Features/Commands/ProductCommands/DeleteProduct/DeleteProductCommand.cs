using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public Contracts.MassTransit.Messages.Commands.DeleteProduct Payload { get; }
    
    public DeleteProductCommand(Contracts.MassTransit.Messages.Commands.DeleteProduct payload)
    {
        Payload = payload;
    }
    
}