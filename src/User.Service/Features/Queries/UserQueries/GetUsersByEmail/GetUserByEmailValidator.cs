using FluentValidation;

namespace User.Service.Features.Queries.UserQueries.GetUsersByEmail;

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