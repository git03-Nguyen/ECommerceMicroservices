namespace Basket.Service.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base("Forbidden access")
    {
    }
}