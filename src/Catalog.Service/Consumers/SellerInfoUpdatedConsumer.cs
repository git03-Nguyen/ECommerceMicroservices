using Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class SellerInfoUpdatedConsumer : IConsumer<IUserInfoUpdated>
{
    private readonly IMediator _mediator;

    public SellerInfoUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserInfoUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateSellerInfoCommand(message));
    }
}