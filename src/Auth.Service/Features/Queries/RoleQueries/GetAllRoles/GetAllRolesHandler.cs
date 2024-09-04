using Auth.Service.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Features.Queries.RoleQueries.GetAllRoles;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, GetAllRolesResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetAllRolesHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<GetAllRolesResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        if (_roleManager.SupportsQueryableRoles)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            return new GetAllRolesResponse(roles);
        }
        else
        {
            throw new NotSupportedException("This role manager does not support querying roles.");
        }
    }
}