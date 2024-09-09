using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.AddNewRole;

public class AddNewRoleHandler : IRequestHandler<AddNewRoleCommand, AddNewRoleResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<AddNewRoleHandler> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AddNewRoleHandler(ILogger<AddNewRoleHandler> logger, RoleManager<ApplicationRole> roleManager,
        IIdentityService identityService)
    {
        _logger = logger;
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task<AddNewRoleResponse> Handle(AddNewRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if user is Admin
        var isAdmin = _identityService.IsUserAdmin();
        if (!isAdmin) throw new UnAuthorizedAccessException();

        // Check if role already exists
        var roleExist = await _roleManager.RoleExistsAsync(request.Payload.Name);
        if (roleExist) throw new ResourceAlreadyExistsException("Role", request.Payload.Name);

        // Create new role
        var role = new ApplicationRole
        {
            Name = request.Payload.Name
        };

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded) throw new Exception("Failed to create role: " + result.Errors);

        _logger.LogInformation("Role {RoleName} created", role.Name);
        return new AddNewRoleResponse(role);
    }
}