using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using Contracts.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Features.Queries.RoleQueries.GetAllRoles;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, GetAllRolesResponse>
{
    private readonly IIdentityService _identityService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetAllRolesHandler(RoleManager<ApplicationRole> roleManager, IIdentityService identityService)
    {
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task<GetAllRolesResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        if (!_roleManager.SupportsQueryableRoles)
            throw new NotSupportedException("This role manager does not support querying roles.");

        var userInfo = _identityService.GetUserInfoIdentity();
        if (userInfo.Role != ApplicationRoleConstants.Admin)
            throw new UnAuthorizedAccessException();

        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        return new GetAllRolesResponse(roles);
    }
}