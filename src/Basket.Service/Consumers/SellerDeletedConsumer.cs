using Basket.Service.Features.Commands.SellerCommands.DeleteSeller;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

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