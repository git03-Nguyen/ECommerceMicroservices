using Basket.Service.Features.Commands.ProductCommands.UpdateProductInfo;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class ProductInfoUpdatedConsumer : IConsumer<ProductInfoUpdated>
{
    private readonly IMediator _mediator;

    public ProductInfoUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductInfoUpdated> context)
    {
        var productInfoUpdated = context.Message;
        await _mediator.Send(new UpdateProductInfoCommand(productInfoUpdated));
    }
}