using Customer.Service.Repositories;
using MediatR;

namespace Customer.Service.Features.Queries.GetAllCustomers;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Models.Customer>>
{
    private readonly ILogger<GetAllCustomersHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersHandler(ICustomerRepository customerRepository, ILogger<GetAllCustomersHandler> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public Task<IEnumerable<Models.Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return _customerRepository.GetAll();
    }
}