using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public SignUpCommand(SignUpRequest payload)
    {
        Payload = payload;
    }

    public SignUpRequest Payload { get; set; }
}