using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public DeleteCustomerCommand(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; set; }
}