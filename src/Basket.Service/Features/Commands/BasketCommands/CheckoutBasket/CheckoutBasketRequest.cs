namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketRequest
{
    public string FullName { get; set; }
    public string ShippingAddress { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsSaveAddress { get; set; } = false;
}