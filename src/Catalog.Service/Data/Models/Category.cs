using Contracts.Models.Interfaces;

namespace Catalog.Service.Data.Models;

public class Category : ISoftDelete
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}