using FluentValidation;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is invalid");
        
        RuleFor(x => x.Payload.Username)
            .NotEmpty().WithMessage("Username cannot be empty")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Username must contain only letters and numbers")    
            .MaximumLength(50).WithMessage("Username must be at most 50 characters long");
        
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