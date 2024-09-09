using Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;
using Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class AccountDeletedConsumer : IConsumer<AccountDeleted>
{
    private readonly IMediator _mediator;

    public AccountDeletedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountDeleted> context)
    {
        var message = context.Message;
        if (message.Role != ApplicationRoleConstants.Seller) return;
        await _mediator.Send(new DeleteSellerCommand(message));
    }
}