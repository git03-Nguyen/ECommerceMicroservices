using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class GetAllCategoriesResponse
{
    public IEnumerable<Category> Payload { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    
    public GetAllCategoriesResponse(IEnumerable<Category> categories, int total, int page, int pageSize)
    {
        Payload = categories;
        Total = total;
        Page = page;
        PageSize = pageSize;
    }
    
}