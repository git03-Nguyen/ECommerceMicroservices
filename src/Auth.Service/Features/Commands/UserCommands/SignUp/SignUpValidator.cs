using Contracts.Constants;
using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Payload.UserName)
            .NotNull().WithMessage("Username cannot be null")
            .NotEmpty().WithMessage("Username cannot be empty");

        RuleFor(x => x.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Payload.Password)
            .NotNull().WithMessage("Password cannot be null")
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[!@#$%^&*()-_=+\\[\\]{}|;:,.<>]")
            .WithMessage("Password must contain at least one special character");
        
        RuleFor(x => x.Payload.Roles)
            .NotNull().WithMessage("Roles cannot be null")
            .NotEmpty().WithMessage("Roles cannot be empty")
            .Must(x => x.All(role => ApplicationRoleConstants.AllRoles.Contains(role)))
            .WithMessage("Invalid role");
    }
}