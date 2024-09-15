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
        // Get account id
        var accountId = _identityService.GetUserId();

        // Get orders
        var orders = await _unitOfWork.OrderRepository.GetByCondition(o => o.BuyerId == accountId)
            .ToListAsync(cancellationToken);

        return new GetOwnOrdersResponse(orders);
    }
}