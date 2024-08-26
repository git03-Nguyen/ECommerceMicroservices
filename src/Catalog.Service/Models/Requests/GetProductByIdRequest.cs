namespace Catalog.Service.Models.Requests;

public class GetProductByIdRequest
{
    public int Id { get; }
    
    public GetProductByIdRequest(int id)
    {
        Id = id;
    }
    
    
}