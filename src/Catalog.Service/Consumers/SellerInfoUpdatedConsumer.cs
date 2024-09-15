using Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class SellerInfoUpdatedConsumer : IConsumer<IUserUpdated>
{
    private readonly IMediator _mediator;

    public SellerInfoUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateSellerInfoCommand(message));
    }
}