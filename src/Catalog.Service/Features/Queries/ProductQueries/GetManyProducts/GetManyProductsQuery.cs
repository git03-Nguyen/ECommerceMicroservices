using Catalog.Service.Features.Queries.ProductQueries.GetProducts;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

public class GetManyProductsQuery : IRequest<GetManyProductsResponse>
{
    public GetManyProductsRequest Payload { get; set; }
    
    public GetManyProductsQuery(GetManyProductsRequest request)
    {
        Payload = request;
    }
    
}