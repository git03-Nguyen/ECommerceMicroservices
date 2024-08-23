using Customer.Service.Repositories;
using MediatR;

namespace Customer.Service.Features.Commands.DeleteCustomer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        return _customerRepository.Delete(request.Id);
    }
    
}