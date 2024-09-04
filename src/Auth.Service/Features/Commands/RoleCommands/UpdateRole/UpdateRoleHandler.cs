using Auth.Service.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly ILogger<UpdateRoleHandler> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UpdateRoleHandler(ILogger<UpdateRoleHandler> logger, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if role exists
        var role = await _roleManager.FindByNameAsync(request.Payload.Name);
        if (role == null)
        {
            throw new Exception("Role does not exist");
        }
        
        role.Name = request.Payload.NewName;
        
        // Update role
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update role");
        }

        _logger.LogInformation("Role updated from {OldRoleName} to {NewRoleName}", request.Payload.Name, request.Payload.NewName);
        return new UpdateRoleResponse(role);
    }
}