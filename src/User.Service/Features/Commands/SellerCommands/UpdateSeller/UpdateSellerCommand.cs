using MediatR;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerCommand : IRequest<UpdateSellerResponse>
{
    public UpdateSellerCommand(UpdateSellerRequest payload)
    {
        Payload = payload;
    }

    public UpdateSellerRequest Payload { get; set; }
}