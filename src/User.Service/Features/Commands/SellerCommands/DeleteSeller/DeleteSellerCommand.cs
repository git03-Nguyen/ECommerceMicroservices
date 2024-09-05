using MediatR;

namespace User.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerCommand : IRequest
{
    public Guid AccountId { get; set; }
    
    public DeleteSellerCommand(Guid accountId)
    {
        AccountId = accountId;
    }
    
}