using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokeTokenResponse>
{
    public RevokeTokenRequest Payload { get; set; } 
    
    public RevokeTokenCommand(RevokeTokenRequest payload)
    {
        Payload = payload;
    }
    
}