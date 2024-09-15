namespace Basket.Service.Data.Models;

public class Basket
{
    public int BasketId { get; set; }
    public Guid AccountId { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}