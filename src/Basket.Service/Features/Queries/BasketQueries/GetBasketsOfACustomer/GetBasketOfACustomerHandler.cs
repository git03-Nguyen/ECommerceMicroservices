using Basket.Service.Exceptions;
using Basket.Service.Repositories;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;

public class GetBasketOfACustomerHandler : IRequestHandler<GetBasketOfACustomerQuery, GetBasketOfACustomerResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetBasketOfACustomerHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<GetBasketOfACustomerResponse> Handle(GetBasketOfACustomerQuery request,
        CancellationToken cancellationToken)
    {
        // Only customers can get their own basket
        _identityService.EnsureIsCustomer();
        
        // Get the account id from the token
        var accountId = _identityService.GetUserId();

        var basket = _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == accountId);
        var basketList = await basket.ToListAsync(cancellationToken);
        if (!basketList.Any())
            throw new ResourceNotFoundException("Basket.AccountId", accountId.ToString());

        return new GetBasketOfACustomerResponse(basketList.First());
    }
}
