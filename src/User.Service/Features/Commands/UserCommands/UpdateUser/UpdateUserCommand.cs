using MediatR;

namespace User.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest<UpdateUserResponse>
{
    public UpdateUserCommand(UpdateUserRequest payload)
    {
        Payload = payload;
    }

    public UpdateUserRequest Payload { get; set; }
}