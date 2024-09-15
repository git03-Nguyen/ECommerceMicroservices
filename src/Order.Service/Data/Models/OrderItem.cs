namespace Order.Service.Data.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Guid SellerAccountId { get; set; }
    public string SellerAccountName { get; set; }

    public string ProductName { get; set; }
    public string ProductImageUrl { get; set; }
    public decimal ProductPrice { get; set; }

    public int Quantity { get; set; }
}