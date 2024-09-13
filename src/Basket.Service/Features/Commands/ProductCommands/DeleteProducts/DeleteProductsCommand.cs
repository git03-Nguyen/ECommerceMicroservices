using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProducts;

public class DeleteProductsCommand : IRequest
{
    public Contracts.MassTransit.Messages.Commands.DeleteProducts Payload { get; }
    
    public DeleteProductsCommand(Contracts.MassTransit.Messages.Commands.DeleteProducts payload)
    {
        Payload = payload;
    }
    
}