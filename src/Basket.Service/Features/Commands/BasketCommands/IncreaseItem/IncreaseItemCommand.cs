using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.IncreaseItem;

public class IncreaseItemCommand : IRequest<UpdateItemResponse>
{
    public IncreaseItemCommand(UpdateItemRequest payload)
    {
        Payload = payload;
    }

    public UpdateItemRequest Payload { get; }
}