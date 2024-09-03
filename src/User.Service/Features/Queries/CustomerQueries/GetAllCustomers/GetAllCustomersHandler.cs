using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.CustomerQueries.GetAllCustomers;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, GetAllCustomersResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustomersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllCustomersResponse> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.CustomerRepository.GetAll().ToListAsync(cancellationToken);
        return new GetAllCustomersResponse(customers);
    }
}