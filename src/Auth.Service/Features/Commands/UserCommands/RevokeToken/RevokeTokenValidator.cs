using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.RevokeToken;

public class RevokeTokenValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenValidator()
    {
        RuleFor(x => x.Payload).NotNull();
    }
}