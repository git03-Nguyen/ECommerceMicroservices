using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.SellerQueries.GetAllSellers;

public class GetAllSellersResponse
{
    public IEnumerable<SellerDto> Payload { get; set; }
    
    public GetAllSellersResponse(IEnumerable<Seller> sellers)
    {
        Payload = sellers.Select(c => new SellerDto
        {
            Id = c.Id,
            Username = c.Username,
            FullName = c.FullName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            PaymentMethod = c.PaymentMethod
        });
    }
}