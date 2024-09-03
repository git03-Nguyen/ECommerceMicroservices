using MediatR;

namespace Auth.Service.Features.Commands.AuthCommands.SignUp;

public class SignUpCommand : IRequest<SignUpResponse>
{
    public SignupRequest Payload { get; set; }
    
    public SignUpCommand(SignupRequest payload)
    {
        Payload = payload;
    }
    
}