using System.Text.RegularExpressions;
using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.Payload.Password)
            .NotNull()
            .WithMessage("Current password is required")
            .NotEmpty()
            .WithMessage("Current password is required")
            .Must(IsValidPassword)
            .WithMessage("Password is not valid");

        RuleFor(x => x.Payload.NewPassword)
            .NotNull()
            .WithMessage("New password is required")
            .NotEmpty()
            .WithMessage("New password is required")
            .Must(IsValidPassword)
            .WithMessage("Password is not valid");

        RuleFor(x => x.Payload.NewPassword)
            .NotEqual(x => x.Payload.Password)
            .WithMessage("New password must be different from the current password");
    }

    // Check password: 6 minimum characters, 1 uppercase, 1 lowercase, 1 digit, 1 special character
    private static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        if (password.Length < 6) return false;
        // less than 1 uppercase letter
        if (!Regex.IsMatch(password, "[A-Z]")) return false;
        // less than 1 lowercase letter
        if (!Regex.IsMatch(password, "[a-z]")) return false;
        // less than 1 digit
        if (!Regex.IsMatch(password, "[0-9]")) return false;
        // less than 1 special character
        if (!Regex.IsMatch(password, "[^A-Za-z0-9]")) return false;
        return true;
    }
}