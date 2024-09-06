namespace Basket.Service.Models.Dtos;

public class BasketItemDto
{
    public int BasketItemId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Snapshot product
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
}