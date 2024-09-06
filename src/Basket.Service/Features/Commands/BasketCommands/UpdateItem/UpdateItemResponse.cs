using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemResponse
{
    public UpdateItemResponse(Data.Models.Basket basket)
    {
        Payload = new BasketDto(basket);
    }

    public BasketDto Payload { get; set; }
}