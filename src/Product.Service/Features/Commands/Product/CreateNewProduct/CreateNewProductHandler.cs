using MediatR;
using Product.Service.Models;
using Product.Service.Repositories;

namespace Product.Service.Features.Commands.Product.CreateNewProduct;

public class CreateNewProductHandler : IRequestHandler<CreateNewProductCommand, ProductItem>
{
    private readonly IProductItemRepository _productItemRepository;

    public CreateNewProductHandler(IProductItemRepository productItemRepository)
    {
        _productItemRepository = productItemRepository;
    }

    public async Task<ProductItem> Handle(CreateNewProductCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductItem
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        await _productItemRepository.Create(product);
        return product;
    }
}