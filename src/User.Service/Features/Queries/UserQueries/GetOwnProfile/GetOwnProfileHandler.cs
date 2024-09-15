using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using User.Service.Repositories;

namespace User.Service.Features.Queries.UserQueries.GetOwnProfile;

public class GetOwnProfileHandler : IRequestHandler<GetOwnProfileQuery, GetOwnProfileResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetOwnProfileHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetOwnProfileResponse> Handle(GetOwnProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user == null) throw new ResourceNotFoundException("User", userId.ToString());
        return new GetOwnProfileResponse(user);
    }
}