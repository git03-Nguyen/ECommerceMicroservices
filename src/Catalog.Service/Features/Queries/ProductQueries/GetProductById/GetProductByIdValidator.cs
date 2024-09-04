using FluentValidation;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        // TODO: what if ProductId is not integer?
        RuleFor(x => x.ProductId)
            .NotNull()
            .WithMessage("ProductId cannot be null")
            .NotEmpty()
            .WithMessage("ProductId cannot be empty")
            .GreaterThan(0)
            .WithMessage("ProductId should be greater than 0");
    }
}