using Catalog.Service.Data.Models;
using Catalog.Service.Data.Repositories.Product;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement pagination from request.Payload
        return await _productRepository.GetAll();
    }
}