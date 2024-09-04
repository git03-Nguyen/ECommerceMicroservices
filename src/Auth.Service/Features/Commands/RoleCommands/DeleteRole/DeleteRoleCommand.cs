using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.DeleteRole;

public class DeleteRoleCommand : IRequest<DeleteRoleResponse>
{
    public DeleteRoleRequest Payload { get; set; }
    
    public DeleteRoleCommand(DeleteRoleRequest payload)
    {
        Payload = payload;
    }
    
}