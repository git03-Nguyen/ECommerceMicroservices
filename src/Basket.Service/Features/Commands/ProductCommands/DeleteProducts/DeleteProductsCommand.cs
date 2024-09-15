using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProducts;

public class DeleteProductsCommand : IRequest
{
    public Contracts.MassTransit.Messages.Commands.IDeleteProducts Payload { get; }
    
    public DeleteProductsCommand(Contracts.MassTransit.Messages.Commands.IDeleteProducts payload)
    {
        Payload = payload;
    }
    
}