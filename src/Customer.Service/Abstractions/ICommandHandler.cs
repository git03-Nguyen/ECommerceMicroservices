using MediatR;

namespace Customer.Service.Abstractions;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : Customer.Service.Abstractions.ICommand<TResponse>
{
}