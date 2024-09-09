using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerResponse
{
    public UpdateSellerResponse(Seller seller)
    {
        Payload = new SellerDto(seller);
    }

    public SellerDto Payload { get; set; }
}