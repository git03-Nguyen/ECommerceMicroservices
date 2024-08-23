using MediatR;

namespace Customer.Service.Features.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }
    
    public int Id { get; }
}