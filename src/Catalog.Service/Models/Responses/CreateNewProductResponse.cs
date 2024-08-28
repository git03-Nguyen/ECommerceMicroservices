using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class CreateNewProductResponse
{
    public int ProductId { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    
}