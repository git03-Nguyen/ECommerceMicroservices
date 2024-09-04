using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserCommand : IRequest<UpdateUserResponse>
{
    public UpdateUserRequest Payload { get; set; } 
    
    public UpdateUserCommand(UpdateUserRequest payload)
    {
        Payload = payload;
    }
    
}