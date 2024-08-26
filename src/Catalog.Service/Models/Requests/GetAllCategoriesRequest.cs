namespace Catalog.Service.Models.Requests;

public class GetAllCategoriesRequest
{
    int PageSize { get; set; }
    int Page { get; set; }
    
    public GetAllCategoriesRequest(int pageSize, int page)
    {
        PageSize = pageSize;
        Page = page;
    }
    
}