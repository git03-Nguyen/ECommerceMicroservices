using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.SellerQueries.GetAllSellers;

public class GetAllSellersHandler : IRequestHandler<GetAllSellersQuery, GetAllSellersResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSellersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllSellersResponse> Handle(GetAllSellersQuery request, CancellationToken cancellationToken)
    {
        var sellers = await _unitOfWork.SellerRepository.GetAll().ToListAsync(cancellationToken);
        return new GetAllSellersResponse(sellers);
    }
}