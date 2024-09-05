namespace Basket.Service.Models.Dtos;

public class BasketDto
{
    public int? BasketId { get; set; }
    public Guid BuyerId { get; set; }
    public ICollection<BasketItemDto>? BasketItems { get; set; } 
    public decimal TotalPrice { get; set; }
}