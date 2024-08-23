using FluentValidation;

namespace Customer.Service.Features.Commands.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull()
            .WithMessage("Customer cannot be null");

        RuleFor(x => x.Customer.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        // Email validation
        RuleFor(x => x.Customer.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Email is not valid");

        // Phone validation
        RuleFor(x => x.Customer.Phone)
            .NotEmpty()
            .WithMessage("Phone cannot be empty")
            .Matches(@"^\d{10}$") // custom validator
            .WithMessage("Phone is not valid");

        RuleFor(x => x.Customer.Address)
            .NotEmpty()
            .WithMessage("Address cannot be empty");
    }
}