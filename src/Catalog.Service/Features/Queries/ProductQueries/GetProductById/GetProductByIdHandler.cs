using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Interfaces;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly ICatalogUnitOfWork _catalogUnitOfWork;

    public GetProductByIdHandler(ICatalogUnitOfWork catalogUnitOfWork)
    {
        _catalogUnitOfWork = catalogUnitOfWork;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _catalogUnitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product == null) throw new ProductNotFoundException(request.ProductId);

        return new GetProductByIdResponse(product);
    }
}

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int requestId)
    {
        RequestId = requestId;
    }
    public int RequestId { get; }
    
    public override string Message => $"Product with id {RequestId} not found";
    
}