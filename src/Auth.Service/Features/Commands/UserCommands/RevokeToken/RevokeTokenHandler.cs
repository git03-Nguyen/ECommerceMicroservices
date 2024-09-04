using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.RevokeToken;

public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, RevokeTokenResponse>
{
    public Task<RevokeTokenResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}