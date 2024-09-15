using MassTransit;

namespace Contracts.MassTransit.Endpoints.SendEndpoint;

public interface ISendEndpointCustomProvider : ISendEndpointProvider
{
    Task SendMessage<T>(object eventModel, CancellationToken cancellationToken)
        where T : class;
}