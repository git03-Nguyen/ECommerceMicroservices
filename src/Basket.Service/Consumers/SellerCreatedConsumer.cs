using Basket.Service.Features.Commands.SellerCommands.CreateSeller;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MassTransit;
using MediatR;

namespace Basket.Service.Consumers;

public class SellerCreatedConsumer : IConsumer<ISellerCreated>
{
    private readonly IMediator _mediator;

    public SellerCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ISellerCreated> context)
    {
        var message = context.Message;
        await _mediator.Send(new CreateSellerCommand(message));
    }
}