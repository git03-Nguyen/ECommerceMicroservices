using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketResponse
{
    public CreateBasketResponse(Data.Models.Basket basket)
    {
        Payload = new BasketDto
        {
            BasketId = basket.BasketId,
            BuyerId = basket.BuyerId,
            BasketItems = basket.BasketItems.Select(x => new BasketItemDto
            {
                BasketItemId = x.BasketItemId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            }).ToList(),
            TotalPrice = basket.TotalPrice
        };
    }

    public BasketDto Payload { get; set; }
}