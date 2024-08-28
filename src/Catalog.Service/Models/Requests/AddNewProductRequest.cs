namespace Catalog.Service.Models.Requests;

public class AddNewProductRequest
{
    public string Name { get; set; } 
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; } = 0;
    public int CategoryId { get; set; }
    
    public DateTime CreatedDate { get; } = DateTime.Now;
    public DateTime UpdatedDate { get; } = DateTime.Now;
    
    public AddNewProductRequest(string name, string description, string imageUrl, decimal price, int availableStock, int categoryId)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Price = price;
        AvailableStock = availableStock;
        CategoryId = categoryId;
    }
}