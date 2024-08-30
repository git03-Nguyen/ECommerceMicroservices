using MassTransit;
using Microsoft.Extensions.Logging;

namespace Contracts.Masstransit.Core;

public class SendEndpointCustomProvider : ISendEndpointCustomProvider
{
    private readonly ILogger<SendEndpointCustomProvider> _logger;
    private readonly IBusControl _busControl;

    public SendEndpointCustomProvider(ILogger<SendEndpointCustomProvider> logger, IBusControl busControl)
    {
        _logger = logger;
        _busControl = busControl;
    }

    public async Task SendMessage<T>(object eventModel, CancellationToken cancellationToken, string? alternateQueueName = null) 
        where T : class
    {
        try
        {
            // Set the queue name
            string queueName;
            if (string.IsNullOrWhiteSpace(alternateQueueName))
            {
                queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(typeof(T).Name);
            }
            else
            {
                queueName = alternateQueueName;
            }
        
            if (!string.IsNullOrWhiteSpace(queueName))
            {
                var sendEndpoint = await GetSendEndpoint(new Uri($"queue:{queueName}"));
                await sendEndpoint.Send<T>(eventModel, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(SendEndpointCustomProvider)} {nameof(SendMessage)} => {ex.Message}");
        }
    }
    
    public ConnectHandle ConnectSendObserver(ISendObserver observer)
    {
        return _busControl.ConnectSendObserver(observer);
    }

    public async Task<ISendEndpoint> GetSendEndpoint(Uri address)
    {
        return await _busControl.GetSendEndpoint(address);
    }

}