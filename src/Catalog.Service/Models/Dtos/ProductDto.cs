using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int Stock { get; set; }
    
    // Limit the properties to be returned
    public ProductDto(Product product)
    {
        ProductId = product.ProductId;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        ImageUrl = product.ImageUrl;
        Stock = product.Stock;
    }
}