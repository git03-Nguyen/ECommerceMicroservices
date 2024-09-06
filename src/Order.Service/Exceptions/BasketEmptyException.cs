namespace Order.Service.Exceptions;

public class BasketEmptyException : Exception
{
    public int BasketId { get; set; }
    
    public BasketEmptyException(int basketId) : base($"Basket is empty for basket id: {basketId}")
    {
        BasketId = basketId;
    }
}