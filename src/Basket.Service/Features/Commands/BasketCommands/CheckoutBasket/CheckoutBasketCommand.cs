using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketCommand : IRequest
{
    public CheckoutBasketCommand(CheckoutBasketRequest payload)
    {
        Payload = payload;
    }

    public CheckoutBasketRequest Payload { get; }
}