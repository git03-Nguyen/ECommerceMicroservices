using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInValidator : AbstractValidator<LogInCommand>
{
    public LogInValidator()
    {
        RuleFor(p => p.Payload.UserName)
            .NotNull().WithMessage("UserName cannot be null")
            .NotEmpty().WithMessage("UserName cannot be empty");

        RuleFor(p => p.Payload.Password)
            .NotNull().WithMessage("Password cannot be null")
            .NotEmpty().WithMessage("Password cannot be empty");
    }
    
}