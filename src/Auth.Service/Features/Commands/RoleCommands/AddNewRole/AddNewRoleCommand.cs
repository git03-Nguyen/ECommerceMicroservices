using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.AddNewRole;

public class AddNewRoleCommand : IRequest<AddNewRoleResponse>
{
    public AddNewRoleRequest Payload { get; set; }
    
    public AddNewRoleCommand(AddNewRoleRequest payload)
    {
        Payload = payload;
    }
}