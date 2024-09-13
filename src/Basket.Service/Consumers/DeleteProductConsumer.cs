using Basket.Service.Features.Commands.ProductCommands.DeleteProducts;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class DeleteProductConsumer : IConsumer<DeleteProducts>
{
    private readonly IMediator _mediator;

    public DeleteProductConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<DeleteProducts> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteProductsCommand(message));
    }
}