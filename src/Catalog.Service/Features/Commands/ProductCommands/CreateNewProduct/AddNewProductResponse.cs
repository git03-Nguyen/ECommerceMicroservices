using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class AddNewProductResponse
{
    public int ProductId { get; set; }
    
    public AddNewProductResponse(Product product)
    {
        ProductId = product.ProductId;
    }
    
}