namespace Basket.Service.Features.Commands.BasketCommands.CreateBasket;

public class CreateBasketRequest
{
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    
    // Maybe wrong, double check by sending message to the Catalog.Service
    public string InitProductName { get; set; }
    public string InitProductImageUrl { get; set; }
    public decimal InitProductUnitPrice { get; set; }
    
}