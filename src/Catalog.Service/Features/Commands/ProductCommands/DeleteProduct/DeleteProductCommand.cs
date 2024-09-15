using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public DeleteProductCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}