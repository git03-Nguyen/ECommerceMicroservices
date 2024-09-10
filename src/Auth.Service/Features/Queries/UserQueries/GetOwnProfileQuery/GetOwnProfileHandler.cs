using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Queries.UserQueries.GetOwnProfileQuery;

public class GetOwnProfileHandler : IRequestHandler<GetOwnProfileQuery, GetOwnProfileResponse>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetOwnProfileHandler(IIdentityService identityService, UserManager<ApplicationUser> userManager)
    {
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<GetOwnProfileResponse> Handle(GetOwnProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserInfoIdentity().Id;
        var user = await _userManager.FindByIdAsync(userId);
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        return new GetOwnProfileResponse(user, role);
    }
}