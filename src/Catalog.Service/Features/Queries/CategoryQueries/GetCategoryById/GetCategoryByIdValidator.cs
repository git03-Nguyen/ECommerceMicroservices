using FluentValidation;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotNull()
            .WithMessage("CategoryId cannot be null")
            .NotEmpty()
            .WithMessage("CategoryId cannot be empty")
            .GreaterThan(0)
            .WithMessage("CategoryId should be greater than 0");
    }
    
}