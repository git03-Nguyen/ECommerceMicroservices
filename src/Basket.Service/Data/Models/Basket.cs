namespace Basket.Service.Data.Models;

public class Basket
{
    public int BasketId { get; set; }
    public Guid BuyerId { get; set; }
    public List<BasketItem> BasketItems { get; set; } = new();
}