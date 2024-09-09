namespace Order.Service.Features.Commands.AdminCreateOrder;

public class AdminCreateOrderRequest
{
    public int BasketId { get; set; }
    public Guid AccountId { get; set; }
    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public IEnumerable<AdminCreateOrderItemRequest> BasketItems { get; set; }
}

public class AdminCreateOrderItemRequest
{
    public Guid SellerAccountId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}