using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedResponse
{
    public IEnumerable<ProductDto> Payload { get; set; }
    
    public UpdateStockAfterOrderCreatedResponse(IEnumerable<Product> products)
    {
        Payload = products.Select(p => new ProductDto(p));
    }
}