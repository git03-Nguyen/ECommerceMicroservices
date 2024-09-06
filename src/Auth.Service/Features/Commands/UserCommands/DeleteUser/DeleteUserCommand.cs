using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<DeleteUserResponse>
{
    public DeleteUserCommand(DeleteUserRequest payload)
    {
        Payload = payload;
    }

    public DeleteUserRequest Payload { get; set; }
}