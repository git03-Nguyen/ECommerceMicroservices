using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.ResetPassword;

public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
{
    public ResetPasswordRequest Payload { get; set; }
    
    public ResetPasswordCommand(ResetPasswordRequest payload)
    {
        Payload = payload;
    }
}