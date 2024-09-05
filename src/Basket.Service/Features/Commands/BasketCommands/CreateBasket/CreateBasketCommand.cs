using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketCommand : IRequest<CreateBasketResponse>
{
    public CreateBasketRequest Payload { get; set; }
    
    public CreateBasketCommand(CreateBasketRequest payload)
    {
        Payload = payload;
    }
    
}