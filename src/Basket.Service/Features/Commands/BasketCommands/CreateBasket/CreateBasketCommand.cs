using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest
{
    public CreateBasketCommand(IAccountCreated payload)
    {
        Payload = payload;
    }

    public IAccountCreated Payload { get; set; }
}