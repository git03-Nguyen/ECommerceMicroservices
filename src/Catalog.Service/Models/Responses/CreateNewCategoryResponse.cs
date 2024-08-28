namespace Catalog.Service.Models.Responses;

public class CreateNewCategoryResponse 
{
    public int CategoryId { get; set; } 
    public string Name { get; set; } 
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    
}