namespace Basket.Service.Exceptions;

public class ProductOutOfStockException : Exception
{
    public int ProductId { get; set; }
    
    public ProductOutOfStockException(int productId)
    {
        ProductId = productId;
    }
}