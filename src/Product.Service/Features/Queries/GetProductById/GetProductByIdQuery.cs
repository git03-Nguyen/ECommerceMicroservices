using MediatR;
using Product.Service.Models;

namespace Product.Service.Features.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductItem>
{
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}