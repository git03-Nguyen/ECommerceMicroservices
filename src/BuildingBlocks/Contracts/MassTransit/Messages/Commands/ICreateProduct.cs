namespace Contracts.MassTransit.Messages.Commands;

public interface ICreateProduct
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    public Guid SellerId { get; set; }
    public string SellerName { get; set; }
}