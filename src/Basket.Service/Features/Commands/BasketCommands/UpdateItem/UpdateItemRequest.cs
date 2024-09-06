namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemRequest
{
    public int BasketId { get; set; }

    public Guid SellerAccountId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Snapshot product
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; }
}