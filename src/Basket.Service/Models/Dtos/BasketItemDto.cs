namespace Basket.Service.Models.Dtos;

public class BasketItemDto
{
    public int BasketItemId { get; set; }
    public int BasketId { get; set; }
    
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}