using Contracts.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Service.Repositories;
using Order.Service.Services.Identity;

namespace Order.Service.Features.Queries.AdminGetAllOrders;

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
        var isAdmin = _identityService.IsAdmin();
        if (!isAdmin) throw new UnAuthorizedAccessException();

        var orders = await _unitOfWork.OrderRepository.GetAll().ToListAsync(cancellationToken);
        return new AdminGetAllOrdersResponse(orders);
    }
}