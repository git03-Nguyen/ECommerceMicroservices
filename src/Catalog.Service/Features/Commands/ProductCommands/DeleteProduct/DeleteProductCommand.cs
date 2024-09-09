using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }
    
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}