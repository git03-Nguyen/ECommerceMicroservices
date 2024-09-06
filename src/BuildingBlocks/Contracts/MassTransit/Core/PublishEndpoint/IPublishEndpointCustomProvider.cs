using MassTransit;

namespace Contracts.MassTransit.Core.PublishEndpoint;

public interface IPublishEndpointCustomProvider : IPublishEndpointProvider
{
    Task PublishMessage(object eventModel, CancellationToken cancellationToken);
}