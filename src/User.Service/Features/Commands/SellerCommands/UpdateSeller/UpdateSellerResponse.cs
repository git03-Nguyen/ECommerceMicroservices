using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerResponse
{
    public SellerDto Payload { get; set; }
    
    public UpdateSellerResponse(Seller seller)
    {
        Payload = new SellerDto
        {
            Id = seller.SellerId,
            AccountId = seller.AccountId,
            Email = seller.Account.Email,
            UserName = seller.Account.UserName,
            FullName = seller.FullName,
            PhoneNumber = seller.PhoneNumber,
            Address = seller.Address,
            PaymentMethod = seller.PaymentMethod
        };
    }
}