using MediatR;

namespace Shared.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}