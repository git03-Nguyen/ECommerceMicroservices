using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailResponse
{
    public GetSellerByEmailResponse(Seller seller)
    {
        Payload = new SellerDto(seller);
    }

    public SellerDto Payload { get; set; }
}