using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    public GetAllProductsRequest Payload { get; }
    
    public GetAllProductsQuery(GetAllProductsRequest payload)
    {
        Payload = payload;
    }
    
}