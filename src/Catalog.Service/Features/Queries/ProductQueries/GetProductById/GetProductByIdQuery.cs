using Catalog.Service.Data.Models;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}