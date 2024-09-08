using MassTransit;

namespace Contracts.MassTransit.Core.PublishEndpoint;

public interface IPublishEndpointCustomProvider : IPublishEndpointProvider
{
    Task PublishMessage(object eventModel, CancellationToken cancellationToken);

    Task PublishMessage(object eventModel, Action<PublishContext> context, CancellationToken cancellationToken);
}