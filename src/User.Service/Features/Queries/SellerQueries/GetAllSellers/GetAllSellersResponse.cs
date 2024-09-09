using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetAllSellers;

public class GetAllSellersResponse
{
    public GetAllSellersResponse(IEnumerable<Seller> sellers)
    {
        Payload = sellers.Select(s => new SellerDto(s));
    }

    public IEnumerable<SellerDto> Payload { get; set; }
}