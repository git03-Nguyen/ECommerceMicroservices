using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class CreateNewProductResponse
{
    public int ProductId { get; set; }
    public string Name { get; set; } 
    public decimal Price { get; set; }
    public int AvailableStock { get; set; } = 0;
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    
    public int? CategoryId { get; set; } = null;
    public Category? Category { get; set; } = null;
}