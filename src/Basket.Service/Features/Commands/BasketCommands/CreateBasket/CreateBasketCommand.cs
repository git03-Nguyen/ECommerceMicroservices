using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest
{
    public CreateBasketCommand(ICustomerCreated payload)
    {
        Payload = payload;
    }

    public ICustomerCreated Payload { get; set; }
}