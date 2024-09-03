using FluentValidation;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailValidator : AbstractValidator<GetSellerByEmailQuery>
{
    public GetSellerByEmailValidator()
    {
        RuleFor(p => p.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is not valid");
    }
}