using Customer.Service.Repositories;
using MediatR;
using Shared.Abstractions.Messaging;

namespace Customer.Service.Features.Commands.CreateCustomer;

public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, Models.Customer>
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