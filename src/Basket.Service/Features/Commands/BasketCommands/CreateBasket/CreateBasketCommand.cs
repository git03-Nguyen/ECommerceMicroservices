using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest
{
    public CreateBasketCommand(AccountCreated payload)
    {
        Payload = payload;
    }

    public AccountCreated Payload { get; set; }
}