using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductResponse
{
    public UpdateProductResponse(Product product)
    {
        Payload = new ProductDto(product);
    }

    public ProductDto Payload { get; }
}