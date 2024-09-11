using MediatR;

namespace Order.Service.Features.Commands.CheckoutBasket;

public class CheckoutBasketCommand : IRequest
{
    public CheckoutBasketCommand(Contracts.MassTransit.Messages.Commands.CheckoutBasket payload)
    {
        Payload = payload;
    }

    public Contracts.MassTransit.Messages.Commands.CheckoutBasket Payload { get; }
}