using MassTransit;
using Microsoft.Extensions.Logging;

namespace Contracts.MassTransit.Core.PublishEndpoint;

public class PublishEndpointCustomProvider : IPublishEndpointCustomProvider
{
    private readonly IBusControl _busControl;
    private readonly ILogger<PublishEndpointCustomProvider> _logger;

    public PublishEndpointCustomProvider(IBusControl busControl, ILogger<PublishEndpointCustomProvider> logger)
    {
        _busControl = busControl;
        _logger = logger;
    }

    public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
    {
        return _busControl.ConnectPublishObserver(observer);
    }

    public async Task<ISendEndpoint> GetPublishSendEndpoint<T>() where T : class
    {
        return await _busControl.GetSendEndpoint(
            new Uri($"queue:public-{new KebabCaseEndpointNameFormatter(false).SanitizeName(typeof(T).Name)}"));
    }

    public async Task PublishMessage(object eventModel, CancellationToken cancellationToken)
    {
        try
        {
            await _busControl.Publish(eventModel, cancellationToken); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(PublishEndpointCustomProvider)} {nameof(PublishMessage)} => {ex.Message}");
        }
    }
}