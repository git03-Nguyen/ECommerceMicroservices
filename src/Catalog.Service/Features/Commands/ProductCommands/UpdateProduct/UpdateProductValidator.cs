using FluentValidation;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Payload.ProductId)
            .NotNull().WithMessage("Id cannot be null.")
            .NotEmpty().WithMessage("Id cannot be empty.")
            .GreaterThan(0).WithMessage("Id should be greater than 0.");

        RuleFor(x => x.Payload.Name)
            .NotNull().WithMessage("Name cannot be null.")
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("Name should not exceed 50 characters.");

        RuleFor(x => x.Payload.Description)
            .NotNull().WithMessage("Description cannot be null.")
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(500).WithMessage("Description should not exceed 500 characters.");
        //
        // RuleFor(x => x.Payload.ImageUrl)
        //     .Must(x => string.IsNullOrEmpty(x) || Uri.TryCreate(x, UriKind.Absolute, out _))
        //     .WithMessage("ImageUrl is not a valid URL");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.Payload.ImageUrl) || x.Payload.ImageUpload != null)
            .WithMessage("Either ImageUrl or ImageUpload is required.");

        RuleFor(x => x)
            .Must(x => string.IsNullOrEmpty(x.Payload.ImageUrl) || x.Payload.ImageUpload == null)
            .WithMessage("You cannot provide both ImageUrl and ImageUpload.");

        RuleFor(x => x.Payload.Price)
            .NotNull().WithMessage("Price cannot be null.")
            .NotEmpty().WithMessage("Price cannot be empty.")
            .GreaterThan(0).WithMessage("Price should be greater than 0.");

        RuleFor(x => x.Payload.Stock)
            .NotNull().WithMessage("Stock cannot be null.")
            .NotEmpty().WithMessage("Stock cannot be empty.")
            .GreaterThan(0).WithMessage("Stock should be greater than 0.");

        RuleFor(x => x.Payload.CategoryId)
            .NotNull().WithMessage("CategoryId cannot be null.")
            .NotEmpty().WithMessage("CategoryId cannot be empty.")
            .GreaterThan(0).WithMessage("CategoryId should be greater than 0.");
    }
}