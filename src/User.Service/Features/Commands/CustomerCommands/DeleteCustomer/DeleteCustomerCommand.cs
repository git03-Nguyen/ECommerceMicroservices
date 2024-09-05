using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public Guid AccountId { get; set; }
    
    public DeleteCustomerCommand(Guid accountId)
    {
        AccountId = accountId;
    }
    
}