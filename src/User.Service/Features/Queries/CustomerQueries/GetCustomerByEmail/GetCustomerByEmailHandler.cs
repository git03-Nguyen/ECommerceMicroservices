using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailHandler: IRequestHandler<GetCustomerByEmailQuery, GetCustomerByEmailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCustomerByEmailResponse> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCondition(
            c => c.Email == request.Payload.Email
        ).FirstOrDefaultAsync(cancellationToken);
        if (customer == null)
        {
            throw new Exception($"Customer with email {request.Payload.Email} not found");
        }
        return new GetCustomerByEmailResponse(customer);
    }
}