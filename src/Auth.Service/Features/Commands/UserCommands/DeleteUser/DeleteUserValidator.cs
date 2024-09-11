using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null")
            .NotEmpty().WithMessage("UserId cannot be empty");
    }
}