using Catalog.Service.Data.Repositories.Product;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class CreateNewProductHandler : IRequestHandler<CreateNewProductCommand, CreateNewProductResponse>
{
    private readonly IProductRepository _productRepository;

    public CreateNewProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateNewProductResponse> Handle(CreateNewProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Data.Models.Product
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            Price = request.Request.Price,
            CategoryId = request.Request.CategoryId
        };
        
        var createdProduct = await _productRepository.Create(product);
        
        var newProductResponse = new CreateNewProductResponse
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            CategoryId = createdProduct.CategoryId
        };
        
        return newProductResponse;
    }
}