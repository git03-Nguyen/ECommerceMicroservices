using Contracts.Models.Interfaces;

namespace Catalog.Service.Data.Models;

public class Product : ISoftDelete
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    // Snapshot from User.Service
    public Guid SellerId { get; set; }
    public Seller Seller { get; set; }

    public bool IsOwnImage { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}