using MediatR;

namespace Customer.Service.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}