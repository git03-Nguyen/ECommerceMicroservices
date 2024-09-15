using Contracts.Constants;
using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Payload.UserName)
            .NotNull().WithMessage("Username cannot be null")
            .NotEmpty().WithMessage("Username cannot be empty")
            .Matches("^[^\\s]+$").WithMessage("Username cannot contain whitespace");

        RuleFor(x => x.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is invalid")
            .Matches("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}")
            .WithMessage("Email is invalid")
            .Matches("^[^\\s]+$").WithMessage("Email cannot contain whitespace");

        RuleFor(x => x.Payload.Password)
            .NotNull().WithMessage("Password cannot be null")
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^A-Za-z0-9]").WithMessage("Password must contain at least one special character")
            .Matches("^[^\\s]+$").WithMessage("Password cannot contain whitespace");

        RuleFor(x => x.Payload.Role)
            .NotNull().WithMessage("Roles cannot be null")
            .NotEmpty().WithMessage("Roles cannot be empty")
            .Must(x => ApplicationRoleConstants.AllRoles.Contains(x))
            .WithMessage("Invalid role");

        RuleFor(x => x.Payload.FullName)
            .NotNull().WithMessage("Full name cannot be null")
            .NotEmpty().WithMessage("Full name cannot be empty")
            .When(x => x.Payload.Role != ApplicationRoleConstants.Admin);

        RuleFor(x => x.Payload.PhoneNumber)
            .NotNull().WithMessage("Phone number cannot be null")
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .Matches("^[0-9]+$").WithMessage("Phone number must contain only numbers")
            .When(x => x.Payload.Role != ApplicationRoleConstants.Admin);

        RuleFor(x => x.Payload.Address)
            .NotNull().WithMessage("Address cannot be null")
            .NotEmpty().WithMessage("Address cannot be empty")
            .When(x => x.Payload.Role != ApplicationRoleConstants.Admin);
    }
}