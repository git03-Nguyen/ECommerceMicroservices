using FluentValidation;

namespace Auth.Service.Features.Commands.RoleCommands.DeleteRole;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleValidator()
    {
        RuleFor(p => p.Payload.Name)
            .NotNull().WithMessage("Role name cannot be null")
            .NotEmpty().WithMessage("Role name cannot be empty")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Role name cannot contain special characters")
            .MaximumLength(20).WithMessage("Role name must not exceed 20 characters");
    }
}