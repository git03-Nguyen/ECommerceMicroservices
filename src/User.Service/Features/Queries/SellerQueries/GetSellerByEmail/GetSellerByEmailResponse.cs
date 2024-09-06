using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailResponse
{
    public GetSellerByEmailResponse(Seller seller)
    {
        Payload = new SellerDto
        {
            Id = seller.SellerId,
            UserName = seller.Account.UserName,
            FullName = seller.FullName,
            AccountId = seller.AccountId,
            Email = seller.Account.Email,
            PhoneNumber = seller.PhoneNumber,
            Address = seller.Address,
            PaymentMethod = seller.PaymentMethod
        };
    }

    public SellerDto Payload { get; set; }
}