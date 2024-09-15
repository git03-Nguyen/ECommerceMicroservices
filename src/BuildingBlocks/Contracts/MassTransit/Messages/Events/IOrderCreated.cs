namespace Contracts.MassTransit.Messages.Events;

public interface IOrderCreated
{
    public int? BasketId { get; set; }

    public ICollection<IOrderItemCreated> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    // public Guid AccountId { get; set; }
}

public interface IOrderItemCreated
{
    public int ProductId { get; set; }
    public Guid SellerAccountId { get; set; }
    public int Quantity { get; set; }
}