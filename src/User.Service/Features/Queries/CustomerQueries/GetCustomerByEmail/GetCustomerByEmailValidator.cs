using FluentValidation;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailValidator : AbstractValidator<GetCustomerByEmailQuery>
{
    public GetCustomerByEmailValidator()
    {
        RuleFor(p => p.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is not valid");
    }
}