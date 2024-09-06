namespace Basket.Service.Models.Dtos;

public class BasketDto
{
    public BasketDto(Data.Models.Basket basket)
    {
        BasketId = basket.BasketId;
        AccountId = basket.AccountId;
        BasketItems = basket.BasketItems.Select(x => new BasketItemDto
        {
            BasketItemId = x.BasketItemId,
            SellerAccountId = x.SellerAccountId,
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            ImageUrl = x.ImageUrl,
            UnitPrice = x.UnitPrice,
            Quantity = x.Quantity
        }).ToList();
    }

    public int? BasketId { get; set; }
    public Guid AccountId { get; set; }
    public ICollection<BasketItemDto>? BasketItems { get; set; }
}