using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Filters;
using FluentValidation;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        // For PageNumber
        RuleFor(x => x.Payload.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page should be greater than 0");

        // For PageSize
        RuleFor(x => x.Payload.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize should be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize should be less than or equal to 100");

        // For SortBy
        RuleFor(x => x.Payload.SortBy)
            .Must(BeAValidSortBy)
            .WithMessage("Invalid SortBy value");

        // For SortOrder
        RuleFor(x => x.Payload.SortOrder)
            .Must(BeAValidSortOrder)
            .WithMessage("Invalid SortOrder value");

        // For MinPrice
        RuleFor(x => x.Payload.MinPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MinPrice should be greater than or equal to 0");

        // For MaxPrice
        RuleFor(x => x.Payload.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MaxPrice should be greater than or equal to 0");
        
        // For MinPrice and MaxPrice
        RuleFor(x => x.Payload)
            .Must(x => x.MinPrice <= x.MaxPrice)
            .WithMessage("MinPrice should be less than or equal to MaxPrice");

        // For CategoryIds
        RuleFor(x => x.Payload.CategoryIds)
            .Must(BeAValidCategoryIds)
            .WithMessage("Invalid CategoryIds value");
    }

    private bool BeAValidSortBy(string sortBy)
    {
        return sortBy == nameof(Product.ProductId) || sortBy == nameof(Product.Name) || sortBy == nameof(Product.Price);
    }

    private bool BeAValidSortOrder(string sortOrder)
    {
        return sortOrder == FilterConstants.Ascending || sortOrder == FilterConstants.Descending;
    }
    
    private bool BeAValidCategoryIds(string categoryIds)
    {
        return categoryIds == string.Empty || categoryIds.Split(',').All(x => int.TryParse(x, out _));
    }
}