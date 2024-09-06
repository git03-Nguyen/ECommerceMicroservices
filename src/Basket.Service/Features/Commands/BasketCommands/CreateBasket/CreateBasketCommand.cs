using Contracts.MassTransit.Events;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest
{
    public CreateBasketCommand(NewAccountCreated payload)
    {
        Payload = payload;
    }

    public NewAccountCreated Payload { get; set; }
}