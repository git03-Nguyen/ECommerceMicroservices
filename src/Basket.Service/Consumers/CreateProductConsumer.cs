using Basket.Service.Features.Commands.ProductCommands.AddNewProduct;
using Contracts.MassTransit.Messages.Commands;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class CreateProductConsumer: IConsumer<ICreateProduct>
{
    private readonly IMediator _mediator;
    
    public CreateProductConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Consume(ConsumeContext<ICreateProduct> context)
    {
        var message = context.Message;
        await _mediator.Send(new AddNewProductCommand(message));
    }
}