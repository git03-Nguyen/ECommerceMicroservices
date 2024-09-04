using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInCommand : IRequest<LogInResponse>
{
    public LogInRequest Payload { get; set; }
    
    public LogInCommand(LogInRequest payload)
    {
        Payload = payload;
    }
    
}