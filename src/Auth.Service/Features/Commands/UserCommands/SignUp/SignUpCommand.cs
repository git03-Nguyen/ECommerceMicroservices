using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public SignUpCommand(SignupRequest payload)
    {
        Payload = payload;
    }

    public SignupRequest Payload { get; set; }
}