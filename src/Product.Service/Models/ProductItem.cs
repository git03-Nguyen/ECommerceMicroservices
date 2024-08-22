namespace Product.Service.Models;

public class ProductItem
{
    public ProductItem()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}