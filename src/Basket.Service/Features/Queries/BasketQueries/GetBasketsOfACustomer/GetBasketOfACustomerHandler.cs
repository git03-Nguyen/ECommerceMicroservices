using Basket.Service.Exceptions;
using Basket.Service.Repositories;
using Contracts.Constants;
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
        var user = _identityService.GetUserInfoIdentity();
        if (user.Role != ApplicationRoleConstants.Customer) throw new UnAuthorizedAccessException();
        
        // Get the account id from the token
        var accountId = Guid.Parse(user.Id);

        var basket = (_unitOfWork.BasketRepository.GetByCondition(x => x.AccountId.ToString() == user.Id)).FirstOrDefault();
        if (basket == null) throw new ResourceNotFoundException(nameof(basket), accountId.ToString());
        
        return new GetBasketOfACustomerResponse(basket);
    }
}
