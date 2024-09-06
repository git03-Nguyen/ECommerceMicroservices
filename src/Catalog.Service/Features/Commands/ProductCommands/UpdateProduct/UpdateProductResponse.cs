using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductResponse
{
    public ProductDto Payload { get; }
    
    public UpdateProductResponse(Product product)
    {
        Payload = new ProductDto(product);
    }
}