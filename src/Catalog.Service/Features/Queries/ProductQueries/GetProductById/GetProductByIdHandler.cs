using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
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
        if (product == null) throw new ResourceNotFoundException(nameof(Product), request.ProductId.ToString());

        return new GetProductByIdResponse(product);
    }
}
