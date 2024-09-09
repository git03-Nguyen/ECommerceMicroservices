using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

public class GetManyProductsResponse
{
    public IEnumerable<ProductDto> Payload { get; set; }

    public GetManyProductsResponse(IEnumerable<Product> products)
    {
        Payload = products.Select(p => new ProductDto(p));
    }
}