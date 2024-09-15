using FluentValidation;

namespace Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.")
            .NotEmpty().WithMessage("Id must not be empty.")
            .NotNull().WithMessage("Id must not be null.");
    }
}