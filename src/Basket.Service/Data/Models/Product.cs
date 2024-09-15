namespace Basket.Service.Data.Models;

public class Product
{
    public int ProductId { get; set; }
    public Guid SellerId { get; set; }
    public Seller Seller { get; set; }

    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
}