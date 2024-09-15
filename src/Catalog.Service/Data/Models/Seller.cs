namespace Catalog.Service.Data.Models;

public class Seller
{
    public Guid SellerId { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}