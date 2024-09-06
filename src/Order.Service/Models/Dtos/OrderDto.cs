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
        Status = order.Status;
        OrderItems = order.OrderItems.Select(x => new OrderItemDto(x)).ToList();
        TotalPrice = order.TotalPrice;
    }

    public int OrderId { get; set; }

    public Guid BuyerId { get; set; }
    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalPrice { get; set; }
}