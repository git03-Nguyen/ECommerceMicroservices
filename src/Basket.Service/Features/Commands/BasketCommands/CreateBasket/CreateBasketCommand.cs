using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest<CreateBasketResponse>
{
    public CreateBasketCommand(CreateBasketRequest payload)
    {
        Payload = payload;
    }

    public CreateBasketRequest Payload { get; set; }
}