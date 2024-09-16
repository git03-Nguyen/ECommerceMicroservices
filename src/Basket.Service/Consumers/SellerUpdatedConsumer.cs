using Basket.Service.Features.Commands.SellerCommands.UpdateSeller;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class SellerUpdatedConsumer : IConsumer<IUserUpdated>
{
    private readonly IMediator _mediator;

    public SellerUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateSellerCommand(message));
    }
}