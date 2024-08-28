using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Filters;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsRequest : IPagingCreterias
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public string SortBy { get; set; } = nameof(Product.ProductId);
    public string SortOrder { get; set; } = FilterConstants.Ascending;

    public decimal MinPrice { get; set; } = decimal.Zero;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;

    public int? CategoryId { get; set; } = null;

    // Other filters
}