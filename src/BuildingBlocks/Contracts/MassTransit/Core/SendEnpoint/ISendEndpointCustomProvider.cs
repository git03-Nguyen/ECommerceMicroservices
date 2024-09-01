using MassTransit;

namespace Contracts.MassTransit.Core.SendEnpoint;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken) 
        where T : class;
}