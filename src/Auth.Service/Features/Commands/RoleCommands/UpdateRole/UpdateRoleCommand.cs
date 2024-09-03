using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleCommand : IRequest<UpdateRoleResponse>
{
    public UpdateRoleRequest Payload { get; set; }
    
    public UpdateRoleCommand(UpdateRoleRequest payload)
    {
        Payload = payload;
    }
    
}