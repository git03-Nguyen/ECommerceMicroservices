using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
{
    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }

    public int ProductId { get; set; }
}