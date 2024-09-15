using MassTransit;
using Microsoft.Extensions.Logging;

namespace Contracts.MassTransit.Endpoints.SendEndpoint;

public class SendEndpointCustomProvider : ISendEndpointCustomProvider
{
    private readonly IBusControl _busControl;
    private readonly ILogger<SendEndpointCustomProvider> _logger;

    public SendEndpointCustomProvider(ILogger<SendEndpointCustomProvider> logger, IBusControl busControl)
    {
        _logger = logger;
        _busControl = busControl;
    }

    public async Task SendMessage<T>(object eventModel, CancellationToken cancellationToken)
        where T : class
    {
        try
        {
            // Set the queue name
            var queueName = new KebabCaseEndpointNameFormatter(false).SanitizeName(typeof(T).Name);

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