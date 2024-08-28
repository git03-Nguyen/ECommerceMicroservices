using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdResponse
{
    public ProductDto Payload { get; set; }

    public GetProductByIdResponse(Product product)
    {
        Payload = new ProductDto(product);
    }
}