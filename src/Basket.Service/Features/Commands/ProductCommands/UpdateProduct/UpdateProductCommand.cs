using Contracts.MassTransit.Messages.Commands;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductInfo;

public class UpdateProductCommand : IRequest
{
    public UpdateProductCommand(IProductUpdated payload)
    {
        Payload = payload;
    }

    public IProductUpdated Payload { get; set; }
}