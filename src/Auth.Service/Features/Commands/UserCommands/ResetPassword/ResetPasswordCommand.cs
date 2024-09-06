using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.ResetPassword;

public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
{
    public ResetPasswordCommand(ResetPasswordRequest payload)
    {
        Payload = payload;
    }

    public ResetPasswordRequest Payload { get; set; }
}