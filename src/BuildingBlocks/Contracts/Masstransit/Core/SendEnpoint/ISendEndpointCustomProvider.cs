using MassTransit;

namespace Contracts.Masstransit.Core.SendEnpoint;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken) 
        where T : class;
}