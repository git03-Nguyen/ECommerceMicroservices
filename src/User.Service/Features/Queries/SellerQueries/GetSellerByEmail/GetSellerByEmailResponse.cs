using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailResponse
{
    public SellerDto Payload { get; set; }
    
    public GetSellerByEmailResponse(Seller seller)
    {
        Payload = new SellerDto()
        {
            Id = seller.Id,
            Username = seller.Username,
            FullName = seller.FullName,
            Email = seller.Email,
            PhoneNumber = seller.PhoneNumber,
            Address = seller.Address,
            PaymentMethod = seller.PaymentMethod
        };
    }
}