namespace Contracts.MassTransit.Messages.Commands;

public class ProductPriceStockUpdated
{
    public ProductPriceStockUpdated(int productId, decimal price, int stock)
    {
        ProductId = productId;
        Price = price;
        Stock = stock;
    }

    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}