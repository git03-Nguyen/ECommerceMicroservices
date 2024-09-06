using MediatR;

namespace User.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerCommand : IRequest
{
    public DeleteSellerCommand(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; set; }
}