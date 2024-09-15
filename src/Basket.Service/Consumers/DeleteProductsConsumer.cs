using Basket.Service.Features.Commands.ProductCommands.DeleteProducts;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class DeleteProductsConsumer : IConsumer<IDeleteProducts>
{
    private readonly IMediator _mediator;

    public DeleteProductsConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IDeleteProducts> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteProductsCommand(message));
    }
}