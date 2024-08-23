using Customer.Service.Repositories;
using MediatR;

namespace Customer.Service.Features.Queries.GetCustomerById;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Models.Customer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<GetCustomerByIdHandler> _logger;

    public GetCustomerByIdHandler(ILogger<GetCustomerByIdHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<Models.Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetBy(customer => customer.Id == request.Id);
    }
}