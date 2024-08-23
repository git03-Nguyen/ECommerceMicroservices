using MediatR;
using Product.Service.Models;

namespace Product.Service.Features.Commands.CreateNewProduct;

public class CreateNewProductCommand : IRequest<ProductItem>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public CreateNewProductCommand(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    
}