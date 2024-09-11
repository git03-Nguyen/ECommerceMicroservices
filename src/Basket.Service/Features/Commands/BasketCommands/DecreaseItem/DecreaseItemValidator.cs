using FluentValidation;

namespace Basket.Service.Features.Commands.BasketCommands.DecreaseItem;

public class DecreaseItemValidator : AbstractValidator<DecreaseItemCommand>
{
    public DecreaseItemValidator()
    {
        RuleFor(x => x.Payload.UserId)
            .NotNull().WithMessage("BasketId is required")
            .NotEmpty().WithMessage("BasketId is required");

        RuleFor(x => x.Payload.ProductId)
            .NotNull().WithMessage("ProductId is required")
            .NotEmpty().WithMessage("ProductId is required")
            .GreaterThan(0).WithMessage("ProductId is greater than 0");

        RuleFor(x => x.Payload.Quantity)
            .NotNull().WithMessage("Quantity is required")
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity is greater than 0");
    }
}