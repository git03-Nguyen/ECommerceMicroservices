using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsQuery : IRequest<GetProductsResponse>
{
    public GetProductsRequest Payload { get; }
    
    public GetProductsQuery(GetProductsRequest payload)
    {
        Payload = payload;
    }
    
}