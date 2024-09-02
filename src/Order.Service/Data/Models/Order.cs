namespace Order.Service.Data.Models;

public class Order
{
    public int OrderId { get; set; }
    public Guid BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public string PaymentAuthCode { get; set; } = string.Empty;
    public bool IsPaid => !string.IsNullOrEmpty(PaymentAuthCode);

    public decimal TotalPrice => OrderItems.Sum(x => x.Quantity * x.ProductPrice);
}

public enum OrderStatus
{
    Pending,
    Completed,
    Canceled
}