using Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Auth.Service.Consumers;

public class UserInfoUpdatedConsumer : IConsumer<UserInfoUpdated>
{
    private readonly IMediator _mediator;

    public UserInfoUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserInfoUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateAccountInfoCommand(message));
    }
}