using FluentValidation;

namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketValidator()
    {
        RuleFor(x => x.Payload.ProductId)
            .NotNull().WithMessage("ProductId is required")
            .NotEmpty().WithMessage("ProductId is required");
        
        RuleFor(x => x.Payload.ProductQuantity)
            .NotNull().WithMessage("ProductQuantity is required")
            .NotEmpty().WithMessage("ProductQuantity is required")
            .GreaterThan(0).WithMessage("ProductQuantity must be greater than 0");
    }
    
}