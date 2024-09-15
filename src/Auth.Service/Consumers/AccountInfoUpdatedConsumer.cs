using Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Auth.Service.Consumers;

public class AccountInfoUpdatedConsumer : IConsumer<IUserInfoUpdated>
{
    private readonly IMediator _mediator;

    public AccountInfoUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserInfoUpdated> context)
    {
        var message = context.Message;
        await _mediator.Send(new UpdateAccountInfoCommand(message));
    }
}