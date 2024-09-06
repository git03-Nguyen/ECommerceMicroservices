using FluentValidation;

namespace Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;

public class GetBasketOfACustomerValidator : AbstractValidator<GetBasketOfACustomerQuery>
{
    public GetBasketOfACustomerValidator()
    {
        RuleFor(x => x.Payload.AccountId)
            .NotNull()
            .WithMessage("AccountId is required")
            .NotEmpty()
            .WithMessage("AccountId is required");
    }
}