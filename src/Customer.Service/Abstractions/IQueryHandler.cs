using MediatR;

namespace Customer.Service.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : Customer.Service.Abstractions.IQuery<TResponse>
{
}