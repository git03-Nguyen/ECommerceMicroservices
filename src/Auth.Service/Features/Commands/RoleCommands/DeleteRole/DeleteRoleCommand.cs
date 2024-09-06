using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.DeleteRole;

public class DeleteRoleCommand : IRequest<DeleteRoleResponse>
{
    public DeleteRoleCommand(DeleteRoleRequest payload)
    {
        Payload = payload;
    }

    public DeleteRoleRequest Payload { get; set; }
}