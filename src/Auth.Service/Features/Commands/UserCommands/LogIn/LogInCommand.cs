using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInCommand : IRequest<LogInResponse>
{
    public LogInCommand(LogInRequest payload)
    {
        Payload = payload;
    }

    public LogInRequest Payload { get; set; }
}