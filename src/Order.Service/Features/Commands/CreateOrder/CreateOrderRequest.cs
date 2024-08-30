namespace Order.Service.Features.Commands.CreateOrder;

public class CreateOrderRequest
{
    public int BasketId { get; set; }
    public string BuyerId { get; set; }
}