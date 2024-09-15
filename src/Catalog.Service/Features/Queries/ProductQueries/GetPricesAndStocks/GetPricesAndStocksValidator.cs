using FluentValidation;

namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksValidator : AbstractValidator<GetPricesAndStocksQuery>
{
    public GetPricesAndStocksValidator()
    {
        RuleFor(x => x.Payload)
            .NotNull()
            .WithMessage("Payload cannot be null")
            .Must(x => x?.Payload != null)
            .WithMessage("Payload cannot be null")
            .NotEmpty()
            .WithMessage("Payload cannot be empty")
            .Must(x => x?.Payload?.Count() > 0)
            .WithMessage("Payload should contain at least one element")
            .Must(x => x.Payload != null && x.Payload.All(y => y > 0))
            .WithMessage("Each element in the list should be greater than 0");
    }
}