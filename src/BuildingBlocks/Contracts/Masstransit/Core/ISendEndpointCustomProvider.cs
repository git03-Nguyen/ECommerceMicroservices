using MassTransit;

namespace Contracts.Masstransit.Core;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken) where T : class;
}