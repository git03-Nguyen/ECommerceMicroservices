using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsResponse
{
    public GetProductsResponse(IEnumerable<Product> products, int totalPage, int pageNumber, int pageSize)
    {
        Payload = products.Select(product => new ProductDto(product));
        TotalPage = totalPage;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public IEnumerable<ProductDto> Payload { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
}