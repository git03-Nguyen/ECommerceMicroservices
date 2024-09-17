using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public ChangePasswordCommand(ChangePasswordRequest payload)
    {
        Payload = payload;
    }

    public ChangePasswordRequest Payload { get; set; }
}