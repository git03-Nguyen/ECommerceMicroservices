using Catalog.Service.Validation;
using FluentValidation;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryValidator : AbstractValidator<AddNewCategoryCommand>
{
    public AddNewCategoryValidator()
    {
        // For Name
        RuleFor(x => x.Payload.Name)
            .NotNull()
            .WithMessage("Name is required")
            .NotEmpty()
            .WithMessage("Name is required");

        // For Description
        RuleFor(x => x.Payload.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");

        // For ImageUrl
        RuleFor(x => x.Payload.ImageUrl)
            .ImageUrl();
    }
}