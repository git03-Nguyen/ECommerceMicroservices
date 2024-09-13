using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Service.Repositories;

namespace Order.Service.Features.Queries.OrderQueries.AdminGetAllOrders;

public class AdminGetAllOrdersHandler : IRequestHandler<AdminGetAllOrdersQuery, AdminGetAllOrdersResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminGetAllOrdersHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<AdminGetAllOrdersResponse> Handle(AdminGetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        // Check admin
        _identityService.EnsureIsAdmin();

        var orders = await _unitOfWork.OrderRepository.GetAll().ToListAsync(cancellationToken);
        return new AdminGetAllOrdersResponse(orders);
    }
}