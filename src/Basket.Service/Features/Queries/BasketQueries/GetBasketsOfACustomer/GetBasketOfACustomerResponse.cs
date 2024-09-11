using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;

public class GetBasketOfACustomerResponse
{
    public GetBasketOfACustomerResponse(Data.Models.Basket basket)
    {
        Basket = new BasketDto(basket);
    }

    public BasketDto Basket { get; set; }
}