using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailHandler : IRequestHandler<GetCustomerByEmailQuery, GetCustomerByEmailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCustomerByEmailResponse> Handle(GetCustomerByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByCondition(
            c => c.Account.Email == request.Payload.Email
        ).FirstOrDefaultAsync(cancellationToken);
        if (customer == null) throw new ResourceNotFoundException("Email", request.Payload.Email);
        return new GetCustomerByEmailResponse(customer);
    }
}