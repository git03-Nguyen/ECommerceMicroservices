using Catalog.Service.Data.Models;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductResponse
{
    public AddNewProductResponse(Product product)
    {
        ProductId = product.ProductId;
    }

    public int ProductId { get; set; }
}