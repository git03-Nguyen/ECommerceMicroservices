namespace Basket.Service.Models.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }

    // Snapshot product
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; }
}