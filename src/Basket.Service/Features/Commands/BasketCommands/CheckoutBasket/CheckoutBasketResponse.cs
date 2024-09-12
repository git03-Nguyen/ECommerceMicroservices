using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketResponse
{
    public CheckoutBasketResponse(Data.Models.Basket basket)
    {
        Basket = new BasketDto(basket);
    }

    public BasketDto Basket { get; set; }
}