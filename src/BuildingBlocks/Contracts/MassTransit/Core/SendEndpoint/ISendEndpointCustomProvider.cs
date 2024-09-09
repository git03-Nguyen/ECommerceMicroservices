using MassTransit;

namespace Contracts.MassTransit.Core.SendEndpoint;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken)
        where T : class;
}