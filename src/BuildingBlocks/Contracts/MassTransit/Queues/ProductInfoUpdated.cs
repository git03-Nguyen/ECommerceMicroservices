namespace Contracts.MassTransit.Queues;

public class ProductInfoUpdated
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; } 
    
    public ProductInfoUpdated(int productId, string name, string description, string imageUrl, int categoryId)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
    }
    
    
    
}