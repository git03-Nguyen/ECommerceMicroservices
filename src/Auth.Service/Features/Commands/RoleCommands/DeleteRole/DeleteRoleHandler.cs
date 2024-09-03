using Auth.Service.Data.Models;
using MassTransit.Util;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.DeleteRole;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    private readonly ILogger<DeleteRoleHandler> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DeleteRoleHandler(ILogger<DeleteRoleHandler> logger, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if role exists
        var role = await _roleManager.FindByNameAsync(request.Payload.Name);
        if (role == null)
        {
            throw new Exception("Role does not exist");
        }
        
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete role");
        }

        _logger.LogInformation("Role {RoleName} deleted", role.Name);
        return DeleteRoleResponse.Empty;
    }
}