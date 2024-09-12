using Order.Service.Data.Models;

namespace Order.Service.Models.Dtos;

public class OrderItemDto
{
    public OrderItemDto(OrderItem orderItem)
    {
        ProductId = orderItem.ProductId;
        SellerAccountId = orderItem.SellerAccountId;
        ProductName = orderItem.ProductName;
        ImageUrl = orderItem.ProductImageUrl;
        UnitPrice = orderItem.ProductPrice;
        Quantity = orderItem.Quantity;
    }

    public int ProductId { get; set; }
    public Guid SellerAccountId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}