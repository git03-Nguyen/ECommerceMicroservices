using Auth.Service.Data.Models;
using Auth.Service.Exceptions;
using Auth.Service.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly ILogger<UpdateRoleHandler> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IIdentityService _identityService;

    public UpdateRoleHandler(ILogger<UpdateRoleHandler> logger, RoleManager<ApplicationRole> roleManager, IIdentityService identityService)
    {
        _logger = logger;
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task<UpdateRoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if user is Admin
        var isAdmin = _identityService.IsUserAdmin();
        if (!isAdmin)
        {
            throw new UnAuthorizedAccessException();
        }
        
        // Check if role exists
        var role = await _roleManager.FindByNameAsync(request.Payload.Name);
        if (role == null)
        {
            throw new ResourceNotFoundException("Role", request.Payload.Name);
        }
        
        role.Name = request.Payload.NewName;
        
        // Update role
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update role: " + result.Errors);
        }

        _logger.LogInformation("Role updated from {OldRoleName} to {NewRoleName}", request.Payload.Name, request.Payload.NewName);
        return new UpdateRoleResponse(role);
    }
}