using Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;
using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Catalog.Service.Consumers;

public class UserUpdatedConsumer : IConsumer<UserInfoUpdated>
{
    private readonly IMediator _mediator;

    public UserUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserInfoUpdated> context)
    {
        var message = context.Message;
        if (message.Role != ApplicationRoleConstants.Seller) return; // TODO: implement the routing key, so that only receive seller.updated instead of *.updated
        await _mediator.Send(new UpdateSellerInfoCommand(message));
    }
}