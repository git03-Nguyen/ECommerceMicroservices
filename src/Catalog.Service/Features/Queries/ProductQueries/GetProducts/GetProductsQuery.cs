using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsQuery : IRequest<GetProductsResponse>
{
    public GetProductsQuery(GetProductsRequest payload)
    {
        Payload = payload;
    }

    public GetProductsRequest Payload { get; }
}