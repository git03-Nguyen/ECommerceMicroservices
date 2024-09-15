namespace Basket.Service.Exceptions;

public class BasketEmptyException : Exception
{
    public BasketEmptyException(int basketId) : base($"Basket is empty for basket id: {basketId}")
    {
        BasketId = basketId;
    }

    public int BasketId { get; set; }
}