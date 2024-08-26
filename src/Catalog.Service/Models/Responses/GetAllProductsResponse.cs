using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class GetAllProductsResponse
{
    public IEnumerable<Product> Payload { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    public GetAllProductsResponse(IEnumerable<Product> products, int total, int page, int pageSize)
    {
        Payload = products;
        Total = total;
        Page = page;
        PageSize = pageSize;
    }
}