using FluentValidation;

namespace Catalog.Service.Validation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character");

        return options;
    }

    public static IRuleBuilderOptions<T, string> ImageUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .MaximumLength(500)
            .WithMessage("ImageUrl must not exceed 500 characters")
            // regex matches http:// .... .jpg or .png
            .Matches(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|png)");

        return options;
    }
}