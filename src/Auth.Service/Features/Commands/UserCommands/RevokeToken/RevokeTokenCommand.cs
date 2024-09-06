using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokeTokenResponse>
{
    public RevokeTokenCommand(RevokeTokenRequest payload)
    {
        Payload = payload;
    }

    public RevokeTokenRequest Payload { get; set; }
}