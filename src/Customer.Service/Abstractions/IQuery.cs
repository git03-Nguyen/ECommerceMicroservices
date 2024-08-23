using MediatR;

namespace Customer.Service.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}