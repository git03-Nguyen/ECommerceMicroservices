namespace Basket.Service.Models.Dtos;

public class BasketDto
{
    public int? BasketId { get; set; }
    public Guid BuyerId { get; set; }
    public List<BasketItemDto>? Items { get; set; } = new();
}