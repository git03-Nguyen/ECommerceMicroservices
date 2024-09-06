namespace Basket.Service.Exceptions;

public class UnAuthorizedAccessException : Exception
{
    public UnAuthorizedAccessException() : base("Unauthorized access")
    {
    }
}