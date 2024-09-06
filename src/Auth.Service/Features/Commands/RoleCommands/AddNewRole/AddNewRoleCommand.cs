using MediatR;

namespace Auth.Service.Features.Commands.RoleCommands.AddNewRole;

public class AddNewRoleCommand : IRequest<AddNewRoleResponse>
{
    public AddNewRoleCommand(AddNewRoleRequest payload)
    {
        Payload = payload;
    }

    public AddNewRoleRequest Payload { get; set; }
}