using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemCommand : IRequest<UpdateItemResponse>
{
    public UpdateItemCommand(UpdateItemRequest payload)
    {
        Payload = payload;
    }

    public UpdateItemRequest Payload { get; }
}