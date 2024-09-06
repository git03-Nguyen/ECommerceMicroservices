using Basket.Service.Models.Dtos;

namespace Basket.Service.Features.Queries.BasketQueries.GetAllBaskets;

public class GetAllBasketsResponse
{
    public GetAllBasketsResponse(IEnumerable<Data.Models.Basket> baskets)
    {
        Payload = baskets.Select(basket => new BasketDto(basket)).ToList();
    }

    public ICollection<BasketDto> Payload { get; set; }
}