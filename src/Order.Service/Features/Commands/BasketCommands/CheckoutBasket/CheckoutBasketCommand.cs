using Contracts.MassTransit.Messages.Commands;
using MediatR;

namespace Order.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketCommand : IRequest
{
    public CheckoutBasketCommand(ICheckoutBasket payload)
    {
        Payload = payload;
    }

    public ICheckoutBasket Payload { get; }
}