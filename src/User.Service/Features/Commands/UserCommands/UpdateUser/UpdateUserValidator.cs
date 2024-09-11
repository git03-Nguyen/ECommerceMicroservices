using FluentValidation;

namespace User.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Payload.Id)
            .NotNull().WithMessage("Id cannot be null")
            .NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is invalid")
            .MaximumLength(50).WithMessage("Email must be at most 50 characters long");

        RuleFor(x => x.Payload.UserName)
            .NotNull().WithMessage("UserName cannot be null")
            .NotEmpty().WithMessage("UserName cannot be empty")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("UserName must contain only letters and numbers")
            .MaximumLength(50).WithMessage("UserName must be at most 50 characters long");

        RuleFor(x => x.Payload.FullName)
            .NotNull().WithMessage("Full name cannot be null")
            .NotEmpty().WithMessage("Full name cannot be empty")
            .MaximumLength(50).WithMessage("Full name must be at most 50 characters long");

        RuleFor(x => x.Payload.PhoneNumber)
            .NotNull().WithMessage("Phone number cannot be null")
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .Matches("^[0-9]*$").WithMessage("Phone number must contain only numbers")
            .MaximumLength(15).WithMessage("Phone number must be at most 15 characters long");

        RuleFor(x => x.Payload.Address)
            .NotNull().WithMessage("Address cannot be null")
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(100).WithMessage("Address must be at most 100 characters long");

        RuleFor(x => x.Payload.PaymentDetails)
            .NotNull().WithMessage("Payment method cannot be null")
            .MaximumLength(50).WithMessage("Payment method must be at most 50 characters long");
    }
}