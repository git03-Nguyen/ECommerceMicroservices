using FluentValidation;

namespace Catalog.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductValidator : AbstractValidator<AddNewProductCommand>
{
    public AddNewProductValidator()
    {
        RuleFor(x => x.Payload.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Payload.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .NotNull()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters");

        RuleFor(x => x.Payload.ImageUrl)
            .NotEmpty()
            .WithMessage("ImageUrl is required")
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            .WithMessage("ImageUrl is not a valid URL");

        RuleFor(x => x.Payload.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .NotNull()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(x => x.Payload.Stock)
            .NotEmpty()
            .WithMessage("Stock is required")
            .NotNull()
            .WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock must be greater than or equal to 0");

        RuleFor(x => x.Payload.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId is required")
            .NotNull()
            .WithMessage("CategoryId is required")
            .GreaterThan(0)
            .WithMessage("CategoryId must be greater than 0");
    }
    
}