using Basket.Service.Features.Commands.BasketCommands.IncreaseItem;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.DecreaseItem;

public class DecreaseItemCommand : IRequest<UpdateItemResponse>
{
    public DecreaseItemCommand(UpdateItemRequest payload)
    {
        Payload = payload;
    }

    public UpdateItemRequest Payload { get; }
}