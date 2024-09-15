using Basket.Service.Features.Commands.ProductCommands.UpdateProduct;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class ProductUpdatedConsumer : IConsumer<IProductUpdated>
{
    private readonly IMediator _mediator;

    public ProductUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IProductUpdated> context)
    {
        var productInfoUpdated = context.Message;
        await _mediator.Send(new UpdateProductCommand(productInfoUpdated));
    }
}