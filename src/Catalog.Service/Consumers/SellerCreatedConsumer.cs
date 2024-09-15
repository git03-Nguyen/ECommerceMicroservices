using Catalog.Service.Features.Commands.SellerCommands.CreateSeller;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class SellerCreatedConsumer : IConsumer<IAccountCreated>
{
    private readonly IMediator _mediator;

    public SellerCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IAccountCreated> context)
    {
        var message = context.Message;
        await _mediator.Send(new CreateSellerCommand(message));
    }
}