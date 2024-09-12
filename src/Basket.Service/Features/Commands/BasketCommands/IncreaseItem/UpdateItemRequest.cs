namespace Basket.Service.Features.Commands.BasketCommands.IncreaseItem;

public class UpdateItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}