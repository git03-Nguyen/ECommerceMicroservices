namespace Catalog.Service.Models.Requests;

public class GetAllProductsRequest
{
    public int PageSize { get; set; }
    public int Page { get; set; }
    
    public GetAllProductsRequest(int pageSize, int page)
    {
        PageSize = pageSize;
        Page = page;
    }
}