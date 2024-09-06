using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleCommand : IRequest<UpdateRoleResponse>
{
    public UpdateRoleCommand(UpdateRoleRequest payload)
    {
        Payload = payload;
    }

    public UpdateRoleRequest Payload { get; set; }
}