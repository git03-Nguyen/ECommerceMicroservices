namespace Catalog.Service.Data.Models;

public class Category
{
    public int CategoryId { get; set; } 
    public string Name { get; set; }    
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}