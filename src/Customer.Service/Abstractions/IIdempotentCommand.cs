namespace Customer.Service.Abstractions;

public interface IIdempotentCommand<out TResponse> : Customer.Service.Abstractions.ICommand<TResponse>
{
    Guid RequestId { get; }
}