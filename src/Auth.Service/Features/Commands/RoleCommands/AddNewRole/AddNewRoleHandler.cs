using Auth.Service.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.AddNewRole;

public class AddNewRoleHandler : IRequestHandler<AddNewRoleCommand, AddNewRoleResponse>
{
    private readonly ILogger<AddNewRoleHandler> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AddNewRoleHandler(ILogger<AddNewRoleHandler> logger, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task<AddNewRoleResponse> Handle(AddNewRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if role already exists
        var roleExist = await _roleManager.RoleExistsAsync(request.Payload.Name);
        if (roleExist)
        {
            throw new Exception("Role already exists");
        }
        
        // Create new role
        var role = new ApplicationRole
        {
            Name = request.Payload.Name
        };
        
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create role");
        }
        
        _logger.LogInformation("Role {RoleName} created", role.Name);
        return new AddNewRoleResponse(role);
    }
}