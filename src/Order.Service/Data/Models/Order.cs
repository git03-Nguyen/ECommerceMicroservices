using Contracts.Interfaces;

namespace Order.Service.Data.Models;

public class Order : ISoftDelete
{
    public int OrderId { get; set; }

    public Guid BuyerId { get; set; }
    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public decimal TotalPrice { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}