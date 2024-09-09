using Basket.Service.Features.Commands.ProductCommands.DeleteProduct;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class DeleteProductConsumer : IConsumer<DeleteProduct>
{
    private readonly IMediator _mediator;

    public DeleteProductConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<DeleteProduct> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteProductCommand(message));
    }
}