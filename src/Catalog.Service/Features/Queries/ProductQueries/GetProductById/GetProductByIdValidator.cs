using FluentValidation;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        // TODO: what if Id is not integer?
        RuleFor(x => x.ProductId)
            .NotNull()
            .WithMessage("Id cannot be null")
            .NotEmpty()
            .WithMessage("Id cannot be empty")
            .GreaterThan(0)
            .WithMessage("Id should be greater than 0");
    }
}