using Basket.Service.Exceptions;
using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
using Contracts.Exceptions;
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
        // Check if owner of the basket is the same as the account id
        var isOwner = _identityService.IsResourceOwner(request.Payload.AccountId);
        if (!isOwner) throw new UnAuthorizedAccessException();

        var basket = _unitOfWork.BasketRepository.GetByCondition(x => x.AccountId == request.Payload.AccountId);
        var basketList = await basket.ToListAsync(cancellationToken);
        if (!basketList.Any())
            throw new ResourceNotFoundException("Basket.AccountId", request.Payload.AccountId.ToString());

        return new GetBasketOfACustomerResponse(basketList.First());
    }
}
