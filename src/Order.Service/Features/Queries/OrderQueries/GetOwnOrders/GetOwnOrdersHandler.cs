using Contracts.Constants;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Service.Repositories;

namespace Order.Service.Features.Queries.OrderQueries.GetOwnOrders;

public class GetOwnOrdersHandler : IRequestHandler<GetOwnOrdersQuery, GetOwnOrdersResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetOwnOrdersHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetOwnOrdersResponse> Handle(GetOwnOrdersQuery request, CancellationToken cancellationToken)
    {
        // Get current user
        var user = _identityService.GetUserInfoIdentity();

        if (user.Role == ApplicationRoleConstants.Customer)
        {
            var orders = await _unitOfWork.OrderRepository.GetByCondition(o => o.BuyerId == Guid.Parse(user.Id))
                .ToListAsync(cancellationToken);
            return new GetOwnOrdersResponse(orders);
        }

        if (user.Role == ApplicationRoleConstants.Seller)
        {
            var orders = await _unitOfWork.OrderRepository
                .GetByCondition(o => o.OrderItems.Any(oi => oi.SellerAccountId == Guid.Parse(user.Id)))
                .ToListAsync(cancellationToken);
            return new GetOwnOrdersResponse(orders);
        }

        throw new BadHttpRequestException("Unknown user role");
    }
}