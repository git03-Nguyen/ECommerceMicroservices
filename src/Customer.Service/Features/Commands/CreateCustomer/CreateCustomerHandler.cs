using Customer.Service.Repositories;
using MediatR;

namespace Customer.Service.Features.Commands.CreateCustomer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Models.Customer>
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