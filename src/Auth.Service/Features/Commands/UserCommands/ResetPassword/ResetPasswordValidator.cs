using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Payload).NotNull();
    }
}