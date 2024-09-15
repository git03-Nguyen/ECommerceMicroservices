using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public ChangePasswordRequest Payload { get; set; } 
    
    public ChangePasswordCommand(ChangePasswordRequest payload)
    {
        Payload = payload;
    }
    
}