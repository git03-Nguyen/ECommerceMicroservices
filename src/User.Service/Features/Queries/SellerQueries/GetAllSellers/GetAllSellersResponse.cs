using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetAllSellers;

public class GetAllSellersResponse
{
    public GetAllSellersResponse(IEnumerable<Seller> sellers)
    {
        Payload = sellers.Select(c => new SellerDto
        {
            Id = c.SellerId,
            UserName = c.Account.UserName,
            FullName = c.FullName,
            AccountId = c.AccountId,
            Email = c.Account.Email,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            PaymentMethod = c.PaymentMethod
        });
    }

    public IEnumerable<SellerDto> Payload { get; set; }
}