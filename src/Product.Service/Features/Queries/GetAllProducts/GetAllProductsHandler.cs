using MediatR;
using Product.Service.Models;
using Product.Service.Repositories;

namespace Product.Service.Features.Queries.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductItem>>
{
    private readonly IProductItemRepository _customerRepository;
    private readonly ILogger<GetAllProductsQuery> _logger;

    public GetAllProductsHandler(IProductItemRepository customerRepository, ILogger<GetAllProductsQuery> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public Task<IEnumerable<ProductItem>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return _customerRepository.GetAll();
    }
}