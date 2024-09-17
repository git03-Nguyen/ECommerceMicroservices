using Basket.Service.Repositories;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;

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

        var basket = _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == accountId)
            .FirstOrDefault();
        if (basket == null) throw new ResourceNotFoundException(nameof(basket), accountId.ToString());

        return new GetBasketOfACustomerResponse(basket);
    }
}