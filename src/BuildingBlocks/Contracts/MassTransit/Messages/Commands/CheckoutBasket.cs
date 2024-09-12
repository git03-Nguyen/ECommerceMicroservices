namespace Contracts.MassTransit.Messages.Commands;

public class CheckoutBasket
{
    public int BasketId { get; set; }
    public Guid AccountId { get; set; }
    
    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public IEnumerable<CheckoutBasketItem> BasketItems { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CheckoutBasketItem
{
    public int BasketItemId { get; set; }
    public int BasketId { get; set; }

    public Guid SellerAccountId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}