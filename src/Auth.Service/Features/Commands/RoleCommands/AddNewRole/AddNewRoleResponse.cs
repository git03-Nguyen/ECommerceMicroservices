using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.RoleCommands.AddNewRole;

public class AddNewRoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    
    public AddNewRoleResponse(ApplicationRole role)
    {
        Id = role.Id;
        Name = role.Name;
        NormalizedName = role.NormalizedName;
    }
}