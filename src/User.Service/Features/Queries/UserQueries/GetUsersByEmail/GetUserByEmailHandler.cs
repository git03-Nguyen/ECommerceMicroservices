using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.UserQueries.GetUsersByEmail;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByEmailHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByCondition(
            u => u.Email == request.Payload.Email
        ).FirstOrDefaultAsync(cancellationToken);
        if (user == null) throw new ResourceNotFoundException("Email", request.Payload.Email);

        // CHeck if user is admin or owner
        _identityService.EnsureIsAdminOrOwner(user.UserId);

        return new GetUserByEmailResponse(user);
    }
}