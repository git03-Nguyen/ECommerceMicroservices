namespace Contracts.MassTransit.Queues;

public class CheckoutBasket
{
    public int BasketId { get; set; }
    public Guid BuyerId { get; set; }
    public List<CheckoutBasketItem> CheckoutBasketItems { get; set; } = new();
}

public class CheckoutBasketItem
{
    public int BasketItemId { get; set; }
    
    public int BasketId { get; set; }
    
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}