using MediatR;

namespace Shared.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}