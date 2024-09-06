using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleResponse
{
    public UpdateRoleResponse(ApplicationRole role)
    {
        Id = role.Id;
        Name = role.Name;
        NormalizedName = role.NormalizedName;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
}