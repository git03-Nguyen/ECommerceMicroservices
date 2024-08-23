using MediatR;
using Product.Service.Models;
using Product.Service.Repositories;

namespace Product.Service.Features.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductItem>
{
    private readonly IProductItemRepository _productItemRepository;

    public GetProductByIdHandler(IProductItemRepository productItemRepository)
    {
        _productItemRepository = productItemRepository;
    }

    public async Task<ProductItem> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productItemRepository.GetBy(p => p.Id == request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(nameof(ProductItem), request.Id);
        }

        return product;
    }
}

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string productItemName, Guid requestId)
    {
        ProductItemName = productItemName;
        RequestId = requestId;
    }
    
    public string ProductItemName { get; }
    public Guid RequestId { get; }
}