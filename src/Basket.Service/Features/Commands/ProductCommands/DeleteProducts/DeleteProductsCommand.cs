using Contracts.MassTransit.Messages.Commands;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.DeleteProducts;

public class DeleteProductsCommand : IRequest
{
    public DeleteProductsCommand(IDeleteProducts payload)
    {
        Payload = payload;
    }

    public IDeleteProducts Payload { get; }
}