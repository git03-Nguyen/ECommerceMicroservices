using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Exceptions;
using User.Service.Repositories;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailHandler : IRequestHandler<GetSellerByEmailQuery, GetSellerByEmailResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSellerByEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetSellerByEmailResponse> Handle(GetSellerByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByCondition(
            c => c.Account.Email == request.Payload.Email
        ).FirstOrDefaultAsync(cancellationToken);
        if (seller == null) throw new ResourceNotFoundException("Email", request.Payload.Email);
        return new GetSellerByEmailResponse(seller);
    }
}