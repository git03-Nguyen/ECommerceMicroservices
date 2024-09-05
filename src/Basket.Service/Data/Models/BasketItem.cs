using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Basket.Service.Data.Models;

public class BasketItem
{
    public int BasketItemId { get; set; }
    
    public int BasketId { get; set; }
    public Basket Basket { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    // Snapshot product
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string ImageUrl { get; set; }
    
    public decimal Price => UnitPrice * Quantity;
    
}