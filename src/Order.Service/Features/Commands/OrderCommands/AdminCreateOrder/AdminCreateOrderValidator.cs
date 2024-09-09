using FluentValidation;

namespace Order.Service.Features.Commands.AdminCreateOrder;

public class AdminCreateOrderValidator : AbstractValidator<AdminCreateOrderCommand>
{
    public AdminCreateOrderValidator()
    {
        RuleFor(x => x.Payload).NotNull();

        // TODO .. Add more validation rules
    }
}