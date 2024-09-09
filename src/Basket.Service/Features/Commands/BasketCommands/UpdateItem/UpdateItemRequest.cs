namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemRequest
{
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}