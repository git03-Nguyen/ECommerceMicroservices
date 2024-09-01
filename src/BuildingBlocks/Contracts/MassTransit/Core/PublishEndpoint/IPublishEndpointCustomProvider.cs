using MassTransit;

namespace Contracts.MassTransit.Core.PublishEndpoint;

public interface IPublishEndpointCustomProvider : IPublishEndpointProvider
{
    Task PublishMessage<T>(object eventModel, CancellationToken cancellationToken) 
        where T : class;
}