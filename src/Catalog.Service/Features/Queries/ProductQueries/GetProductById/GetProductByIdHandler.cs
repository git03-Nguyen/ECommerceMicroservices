using Catalog.Service.Data.Models;
using Catalog.Service.Data.Repositories.Product;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBy(p => p.ProductId == request.Id);
        if (product == null) throw new ProductNotFoundException(nameof(Product), request.Id);
        
        return product;
    }
}

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string productName, int requestId)
    {
        ProductName = productName;
        RequestId = requestId;
    }
    
    public string ProductName { get; }
    public int RequestId { get; }
    
    public override string Message => $"Product {ProductName} with id {RequestId} not found";
    
}