using MassTransit;

namespace Contracts.Masstransit.Core;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken, string? alternateQueueName = null) 
        where T : class;
}