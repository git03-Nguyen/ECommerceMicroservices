using Basket.Service.Exceptions;
using Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;
using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Queries.BasketQueries.GetAllBaskets;

public class GetAllBasketsHandler : IRequestHandler<GetAllBasketsQuery, GetAllBasketsResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBasketsHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllBasketsResponse> Handle(GetAllBasketsQuery request, CancellationToken cancellationToken)
    {
        // Check if admin
        var isAdmin = _identityService.IsAdmin();
        if (!isAdmin) throw new UnAuthorizedAccessException();

        var baskets = _unitOfWork.BasketRepository.GetAll();
        var basketList = await baskets.ToListAsync(cancellationToken);

        return new GetAllBasketsResponse(basketList);
    }
}