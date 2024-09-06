using FluentValidation;

namespace Auth.Service.Features.Commands.RoleCommands.UpdateRole;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleValidator()
    {
        ApplyRoleNameRules(() => RuleFor(p => p.Payload.Name));
        ApplyRoleNameRules(() => RuleFor(p => p.Payload.NewName));
    }

    private void ApplyRoleNameRules(Func<IRuleBuilderInitial<UpdateRoleCommand, string>> ruleFor)
    {
        ruleFor()
            .NotNull().WithMessage("Role name cannot be null")
            .NotEmpty().WithMessage("Role name cannot be empty")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Role name cannot contain special characters")
            .MaximumLength(20).WithMessage("Role name must not exceed 20 characters");
    }
}