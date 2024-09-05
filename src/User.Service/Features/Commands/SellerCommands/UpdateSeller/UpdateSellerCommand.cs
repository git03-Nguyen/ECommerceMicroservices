using MediatR;
using User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerCommand : IRequest<UpdateSellerResponse>
{
    public UpdateSellerRequest Payload { get; set; }
    
    public UpdateSellerCommand(UpdateSellerRequest payload)
    {
        Payload = payload;
    }
    
}