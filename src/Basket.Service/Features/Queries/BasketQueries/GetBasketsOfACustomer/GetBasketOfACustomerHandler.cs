using Basket.Service.Repositories;
using Basket.Service.Services.Identity;
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
        if (!basketList.Any()) throw new BasketNotFoundException(request.Payload.AccountId);

        return new GetBasketOfACustomerResponse(basketList.First());
    }
}

public class UnAuthorizedAccessException : Exception
{
    public UnAuthorizedAccessException() : base("You are not authorized to access this resource")
    {
    }
}

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException(Guid accountId) : base($"Basket not found for account {accountId}")
    {
        AccountId = accountId;
    }

    public BasketNotFoundException(int basketId) : base($"Basket not found for basket {basketId}")
    {
    }

    public Guid AccountId { get; }
}