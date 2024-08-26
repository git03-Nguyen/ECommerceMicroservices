using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Responses;

public class GetProductByIdResponse
{
    public int ProductId { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 
    public string ImageUrl { get; set; } 
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; } 
    
    public int? CategoryId { get; set; }
    public Category? Category { get; set; } 
}