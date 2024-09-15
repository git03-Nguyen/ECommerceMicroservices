namespace Contracts.MassTransit.Messages.Commands;

public interface ICheckoutBasket
{
    public int BasketId { get; set; }
    public Guid AccountId { get; set; }

    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public IEnumerable<ICheckoutBasketItem> BasketItems { get; set; }
    public decimal TotalPrice { get; set; }
}

public interface ICheckoutBasketItem
{
    public int BasketItemId { get; set; }
    public int BasketId { get; set; }

    public Guid SellerAccountId { get; set; }
    public string SellerAccountName { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}