using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
{
    public int ProductId { get; set; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}