using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Dtos;

public class ProductPriceStockDto
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    public ProductPriceStockDto(Product product)
    {
        ProductId = product.ProductId;
        Price = product.Price;
        Stock = product.Stock;
    }
}