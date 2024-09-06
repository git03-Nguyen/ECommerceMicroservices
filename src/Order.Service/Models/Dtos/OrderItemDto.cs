using Order.Service.Data.Models;

namespace Order.Service.Models.Dtos;

public class OrderItemDto
{
    public OrderItemDto(OrderItem orderItem)
    {
        OrderItemId = orderItem.OrderItemId;
        ProductId = orderItem.ProductId;
        SellerAccountId = orderItem.SellerAccountId;
        ProductName = orderItem.ProductName;
        ProductImageUrl = orderItem.ProductImageUrl;
        ProductPrice = orderItem.ProductPrice;
        Quantity = orderItem.Quantity;
    }

    public int OrderItemId { get; set; }

    public int ProductId { get; set; }
    public Guid SellerAccountId { get; set; }
    public string ProductName { get; set; }
    public string ProductImageUrl { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}