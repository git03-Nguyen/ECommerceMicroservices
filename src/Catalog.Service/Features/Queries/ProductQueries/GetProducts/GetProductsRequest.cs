using Catalog.Service.Data.Models;
using Catalog.Service.Models.Filters;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsRequest : IPagingCreterias
{
    public string SortBy { get; set; } = nameof(Product.ProductId);
    public string SortOrder { get; set; } = FilterConstants.Ascending;

    public decimal MinPrice { get; set; } = decimal.Zero;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string SearchTerm { get; set; } = string.Empty;

    public string CategoryIds { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Other filters
}