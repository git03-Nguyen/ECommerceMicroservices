using Contracts.MassTransit.Queues;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductInfo;

public class UpdateProductInfoCommand : IRequest
{
    public UpdateProductInfoCommand(ProductInfoUpdated payload)
    {
        Payload = payload;
    }

    public ProductInfoUpdated Payload { get; set; }
}