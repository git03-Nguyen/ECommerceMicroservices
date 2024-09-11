using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest<DeleteUserResponse>
{
    public DeleteUserCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}