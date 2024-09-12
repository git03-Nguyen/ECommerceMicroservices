using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketCommand : IRequest<CheckoutBasketResponse>
{
    public CheckoutBasketCommand(CheckoutBasketRequest payload)
    {
        Payload = payload;
    }

    public CheckoutBasketRequest Payload { get; }
}