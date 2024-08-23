using MediatR;

namespace Customer.Service.Features.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<Models.Customer>
{
    public CreateCustomerCommand(Models.Customer customer)
    {
        Customer = customer;
    }
    
    public Models.Customer Customer { get; }
}