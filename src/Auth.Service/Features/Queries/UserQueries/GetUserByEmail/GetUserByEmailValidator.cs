using FluentValidation;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailValidator()
    {
        RuleFor(p => p.Payload.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Email is not valid");
    }
}