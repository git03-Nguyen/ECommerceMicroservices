using Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;
using Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class SellerDeletedConsumer : IConsumer<IAccountDeleted>
{
    private readonly IMediator _mediator;

    public SellerDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountDeleted> context)
    {
        var message = context.Message;
        await _mediator.Send(new DeleteSellerCommand(message));
    }
}