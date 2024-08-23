namespace Shared.Abstractions.Messaging;

public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
{
    Guid RequestId { get; }
}