namespace Contracts.MassTransit.Events;

public class OrderCreated
{
    public int? BasketId { get; set; }

    public ICollection<OrderItemCreated> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    // public Guid AccountId { get; set; }
}

public class OrderItemCreated
{
    public int ProductId { get; set; }
    public Guid SellerAccountId { get; set; }
    public int Quantity { get; set; }
}