using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateStockAfterOrderCreated;

public class UpdateStockAfterOrderCreatedResponse
{
    public UpdateStockAfterOrderCreatedResponse(IEnumerable<Product> products)
    {
        Payload = products.Select(p => new ProductDto(p));
    }

    public IEnumerable<ProductDto> Payload { get; set; }
}