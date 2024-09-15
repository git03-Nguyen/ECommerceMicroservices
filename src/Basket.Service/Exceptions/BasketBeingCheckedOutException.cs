namespace Basket.Service.Exceptions;

public class BasketBeingCheckedOutException : Exception
{
    public BasketBeingCheckedOutException(int basketId) : base("Basket is being checked out")
    {
        BasketId = basketId;
    }

    public int BasketId { get; set; }
}