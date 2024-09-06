using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;

public class GetBasketOfACustomerResponse
{
    public GetBasketOfACustomerResponse(Data.Models.Basket basket)
    {
        Payload = new BasketDto(basket);
    }

    public BasketDto Payload { get; set; }
}