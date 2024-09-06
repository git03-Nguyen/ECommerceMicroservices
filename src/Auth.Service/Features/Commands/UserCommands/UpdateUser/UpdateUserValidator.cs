using FluentValidation;

namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Payload.Id)
            .NotNull().WithMessage("Id is required")
            .NotEmpty().WithMessage("Id is required")
            .MaximumLength(50).WithMessage("Id must not exceed 50 characters");

        // username can be null but if it is not null it must not exceed 50 characters
        RuleFor(x => x.Payload.UserName)
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

        // email can be null but if it is not null it must not exceed 50 characters
        RuleFor(x => x.Payload.Email)
            .EmailAddress().WithMessage("Email is not valid")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters");
    }
}