using FluentValidation;
using User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateSellerValidator()
    {
        RuleFor(x => x.Payload.Id)
            .NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x.Payload.AccountId)
            .NotNull().WithMessage("AccountId cannot be null")
            .NotEmpty().WithMessage("AccountId cannot be empty");

        RuleFor(x => x.Payload.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is invalid")
            .MaximumLength(50).WithMessage("Email must be at most 50 characters long");

        RuleFor(x => x.Payload.UserName)
            .NotEmpty().WithMessage("UserName cannot be empty")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("UserName must contain only letters and numbers")
            .MaximumLength(50).WithMessage("UserName must be at most 50 characters long");

        RuleFor(x => x.Payload.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty")
            .MaximumLength(50).WithMessage("Full name must be at most 50 characters long");

        RuleFor(x => x.Payload.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .Matches("^[0-9]*$").WithMessage("Phone number must contain only numbers")
            .MaximumLength(15).WithMessage("Phone number must be at most 15 characters long");

        RuleFor(x => x.Payload.Address)
            .NotEmpty().WithMessage("Address cannot be empty")
            .MaximumLength(100).WithMessage("Address must be at most 100 characters long");

        RuleFor(x => x.Payload.PaymentMethod)
            .NotEmpty().WithMessage("Payment method cannot be empty")
            .MaximumLength(50).WithMessage("Payment method must be at most 50 characters long");
    }
}