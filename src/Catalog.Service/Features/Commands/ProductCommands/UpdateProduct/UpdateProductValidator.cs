using FluentValidation;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Request.ProductId)
            .NotNull().WithMessage("ProductId cannot be null.")
            .NotEmpty().WithMessage("ProductId cannot be empty.")
            .GreaterThan(0).WithMessage("ProductId should be greater than 0.");
        
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("Name should not exceed 50 characters.");
        
        RuleFor(x => x.Request.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(500).WithMessage("Description should not exceed 500 characters.");
        
        RuleFor(x => x.Request.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl cannot be empty.")
            .MaximumLength(500).WithMessage("ImageUrl should not exceed 500 characters.");
        
        RuleFor(x => x.Request.Price)
            .NotNull().WithMessage("Price cannot be null.")
            .NotEmpty().WithMessage("Price cannot be empty.")
            .GreaterThan(0).WithMessage("Price should be greater than 0.");
        
        RuleFor(x => x.Request.Stock)
            .NotEmpty().WithMessage("Stock cannot be empty.")
            .GreaterThan(0).WithMessage("Stock should be greater than 0.");
        
        RuleFor(x => x.Request.CategoryId)
            .NotEmpty().WithMessage("CategoryId cannot be empty.")
            .GreaterThan(0).WithMessage("CategoryId should be greater than 0.");
    }
}