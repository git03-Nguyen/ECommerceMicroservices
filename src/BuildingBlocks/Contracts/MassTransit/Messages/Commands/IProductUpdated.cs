namespace Contracts.MassTransit.Messages.Commands;

public interface IProductUpdated
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }
    public int Stock { get; set; }
}