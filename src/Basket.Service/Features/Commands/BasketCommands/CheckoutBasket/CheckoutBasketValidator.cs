using FluentValidation;

namespace Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.Payload.BasketId)
            .NotNull().WithMessage("BasketId is required")
            .NotEmpty().WithMessage("BasketId is required")
            .GreaterThan(0).WithMessage("BasketId is greater than 0");

        RuleFor(x => x.Payload.RecipientName)
            .NotNull().WithMessage("RecipientName is required")
            .NotEmpty().WithMessage("RecipientName is required")
            .MaximumLength(50).WithMessage("RecipientName is greater than 50");

        RuleFor(x => x.Payload.ShippingAddress)
            .NotNull().WithMessage("ShippingAddress is required")
            .NotEmpty().WithMessage("ShippingAddress is required")
            .MaximumLength(100).WithMessage("ShippingAddress is not greater than 100");

        RuleFor(x => x.Payload.RecipientPhone)
            .NotNull().WithMessage("RecipientPhone is required")
            .NotEmpty().WithMessage("RecipientPhone is required")
            .Matches(@"^(\+84|0)\d{10,11}$").WithMessage("RecipientPhone is not a valid phone number");
    }
}