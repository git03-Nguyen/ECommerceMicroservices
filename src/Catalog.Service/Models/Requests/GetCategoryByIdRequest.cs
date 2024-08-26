namespace Catalog.Service.Models.Requests;

public class GetCategoryByIdRequest
{
    public int Id { get; }
    
    public GetCategoryByIdRequest(int id)
    {
        Id = id;
    }
    
    
}