namespace Basket.Service.Data.Models;

public class Basket
{
    public int BasketId { get; set; }
    public Guid BuyerId { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

    public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);
}