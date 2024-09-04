using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdResponse
{
    public GetProductByIdResponse(Product product)
    {
        Payload = new ProductDto(product);
    }

    public ProductDto Payload { get; set; }
}