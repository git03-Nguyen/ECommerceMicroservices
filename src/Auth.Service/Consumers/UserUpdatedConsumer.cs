using Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Auth.Service.Consumers;

public class UserUpdatedConsumer : IConsumer<UserUpdated>
{
    private readonly IMediator _mediator;

    public UserUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateAccountInfoCommand(message));
    }
}