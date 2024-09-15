using FluentValidation;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Payload.CategoryId)
            .NotNull()
            .WithMessage("CategoryId is required")
            .NotEmpty()
            .WithMessage("CategoryId is required")
            .GreaterThan(0)
            .WithMessage("CategoryId must be greater than 0");

        RuleFor(x => x.Payload.Name)
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Payload.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");
    }
}