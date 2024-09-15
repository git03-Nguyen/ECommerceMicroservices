namespace Basket.Service.Exceptions;

public class ProductOutOfStockException : Exception
{
    public ProductOutOfStockException(int productId) : base($"Product with id: {productId} is out of stock")
    {
        ProductId = productId;
    }

    public int ProductId { get; set; }
}