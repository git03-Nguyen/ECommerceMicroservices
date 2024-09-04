using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<DeleteUserResponse>
{
    public DeleteUserRequest Payload { get; set; }
    
    public DeleteUserCommand(DeleteUserRequest payload)
    {
        Payload = payload;
    }
    
}