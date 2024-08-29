using Catalog.Service.Data.Models;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductResponse
{
    public int ProductId { get; set; }
    
    public AddNewProductResponse(Product product)
    {
        ProductId = product.ProductId;
    }
    
}