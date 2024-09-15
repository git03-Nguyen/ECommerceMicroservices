using FluentValidation;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {

        RuleFor(x => x.Payload.FullName)
            .NotNull().WithMessage("FullName is required")
            .NotEmpty().WithMessage("FullName is required")
            .MaximumLength(50).WithMessage("FullName is greater than 50");

        RuleFor(x => x.Payload.ShippingAddress)
            .NotNull().WithMessage("ShippingAddress is required")
            .NotEmpty().WithMessage("ShippingAddress is required")
            .MaximumLength(100).WithMessage("ShippingAddress is not greater than 100");

        RuleFor(x => x.Payload.PhoneNumber)
            .NotNull().WithMessage("PhoneNumber is required")
            .NotEmpty().WithMessage("PhoneNumber is required")
            .Matches(@"^\d+$").WithMessage("PhoneNumber is not a number")
            .MaximumLength(15).WithMessage("PhoneNumber is not greater than 15");
        
        
        RuleFor(x => x.Payload.IsSaveAddress)
            .NotNull().WithMessage("IsSaveAddress is required");
    }
}