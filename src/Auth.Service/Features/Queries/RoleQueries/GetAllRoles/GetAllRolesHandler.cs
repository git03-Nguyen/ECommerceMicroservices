using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Features.Queries.RoleQueries.GetAllRoles;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, GetAllRolesResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IIdentityService _identityService;

    public GetAllRolesHandler(RoleManager<ApplicationRole> roleManager, IIdentityService identityService)
    {
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task<GetAllRolesResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        if (!_roleManager.SupportsQueryableRoles) throw new NotSupportedException("This role manager does not support querying roles.");
        
        var userInfo = _identityService.GetUserInfoIdentity();
        if (userInfo.Role != ApplicationRoleConstants.Admin) throw new UnauthorizedAccessException("You are not authorized to perform this action");
        
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
        return new GetAllRolesResponse(roles);
    }
}