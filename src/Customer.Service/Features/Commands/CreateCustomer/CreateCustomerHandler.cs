using Customer.Service.Repositories;

namespace Customer.Service.Features.Commands.CreateCustomer;

public class CreateCustomerHandler : Abstractions.ICommandHandler<CreateCustomerCommand, Models.Customer>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Models.Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        return await _customerRepository.Create(request.Customer);
    }
}