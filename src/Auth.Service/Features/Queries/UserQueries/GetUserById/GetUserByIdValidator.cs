using FluentValidation;

namespace Auth.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required")
            .NotEmpty().WithMessage("Id is required")
            .Must(x => x != Guid.Empty).WithMessage("Id is required");
    }
}