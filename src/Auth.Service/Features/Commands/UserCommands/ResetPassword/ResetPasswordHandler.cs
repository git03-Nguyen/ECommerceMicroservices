using MediatR;

namespace Auth.Service.Features.Commands.UserCommands.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    public Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}