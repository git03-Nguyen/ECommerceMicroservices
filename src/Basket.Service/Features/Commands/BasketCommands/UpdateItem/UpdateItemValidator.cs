using FluentValidation;

namespace Basket.Service.Features.Commands.BasketCommands.UpdateItem;

public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Payload.BasketId)
            .NotNull().WithMessage("BasketId is required")
            .NotEmpty().WithMessage("BasketId is required")
            .GreaterThan(0).WithMessage("BasketId is greater than 0");

        RuleFor(x => x.Payload.ProductId)
            .NotNull().WithMessage("ProductId is required")
            .NotEmpty().WithMessage("ProductId is required")
            .GreaterThan(0).WithMessage("ProductId is greater than 0");

        RuleFor(x => x.Payload.Quantity)
            .NotNull().WithMessage("Quantity is required")
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Quantity is greater than or equal to 0");
    }
}