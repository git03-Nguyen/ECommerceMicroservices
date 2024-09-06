using MediatR;

namespace Order.Service.Features.Commands.CheckoutBasket;

public class CheckoutBasketCommand : IRequest
{
    public CheckoutBasketCommand(Contracts.MassTransit.Queues.CheckoutBasket payload)
    {
        Payload = payload;
    }

    public Contracts.MassTransit.Queues.CheckoutBasket Payload { get; }
}