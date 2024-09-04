using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }

    public UpdateRoleResponse(ApplicationRole role)
    {
        Id = role.Id;
        Name = role.Name;
        NormalizedName = role.NormalizedName;
    } 
}