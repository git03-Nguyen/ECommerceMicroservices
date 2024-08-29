namespace Basket.Service.Models.Requests;

public class AddBasketItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}