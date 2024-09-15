using Order.Service.Data.Models;

namespace Order.Service.Models.Dtos;

public class OrderDto
{
    public OrderDto(Data.Models.Order order)
    {
        OrderId = order.OrderId;
        BuyerId = order.BuyerId;
        RecipientName = order.RecipientName;
        ShippingAddress = order.ShippingAddress;
        RecipientPhone = order.RecipientPhone;
        Status = (Enum.GetName(typeof(OrderStatus), order.Status) ??
                  Enum.GetName(typeof(OrderStatus), OrderStatus.Unknown)) ?? "";
        OrderItems = order.OrderItems.Select(x => new OrderItemDto(x)).ToList();
        TotalPrice = order.TotalPrice;
        CreatedDate = order.CreatedDate;
        UpdatedDate = order.UpdatedDate;
    }

    public int OrderId { get; set; }

    public Guid BuyerId { get; set; }
    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; }

    public string Status { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }

    public decimal TotalPrice { get; set; }
}