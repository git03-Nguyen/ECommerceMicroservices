using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Basket.Service.Data.Models;

public class BasketItem
{
    public int BasketItemId { get; set; }

    public int BasketId { get; set; }

    [NotMapped] [JsonIgnore] public Basket? Basket { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }
}