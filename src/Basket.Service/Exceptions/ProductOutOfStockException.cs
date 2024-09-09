namespace Basket.Service.Exceptions;

public class ProductOutOfStockException : Exception
{
    public int ProductId { get; set; }
    
    public ProductOutOfStockException(int productId): base($"Product with id: {productId} is out of stock")
    {
        ProductId = productId;
    }
}