namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketRequest
{
    public int BasketId { get; set; }

    public string RecipientName { get; set; }
    public string ShippingAddress { get; set; }
    public string RecipientPhone { get; set; }
}