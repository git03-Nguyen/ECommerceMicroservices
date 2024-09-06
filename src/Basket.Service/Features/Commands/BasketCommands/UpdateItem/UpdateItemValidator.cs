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

        RuleFor(x => x.Payload.SellerAccountId)
            .NotNull().WithMessage("SellerAccountId is required")
            .NotEmpty().WithMessage("SellerAccountId is required");

        RuleFor(x => x.Payload.ProductId)
            .NotNull().WithMessage("ProductId is required")
            .NotEmpty().WithMessage("ProductId is required")
            .GreaterThan(0).WithMessage("ProductId is greater than 0");

        RuleFor(x => x.Payload.Quantity)
            .NotNull().WithMessage("Quantity is required")
            .NotEmpty().WithMessage("Quantity is required")
            .NotEqual(0).WithMessage("Quantity is not equal to 0");

        RuleFor(x => x.Payload.ProductName)
            .NotNull().WithMessage("ProductName is required")
            .NotEmpty().WithMessage("ProductName is required")
            .MaximumLength(50).WithMessage("ProductName is greater than 50");

        RuleFor(x => x.Payload.UnitPrice)
            .NotNull().WithMessage("UnitPrice is required")
            .NotEmpty().WithMessage("UnitPrice is required")
            .GreaterThan(0).WithMessage("UnitPrice is greater than 0");

        RuleFor(x => x.Payload.ImageUrl)
            .NotNull().WithMessage("ImageUrl is required")
            .NotEmpty().WithMessage("ImageUrl is required")
            .Matches(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)")
            .WithMessage("ImageUrl is not a valid URL");
    }
}