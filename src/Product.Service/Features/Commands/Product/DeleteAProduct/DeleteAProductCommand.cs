using MediatR;

namespace Product.Service.Features.Commands.Product.DeleteAProduct;

public class DeleteAProductCommand : IRequest
{
    public DeleteAProductCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}