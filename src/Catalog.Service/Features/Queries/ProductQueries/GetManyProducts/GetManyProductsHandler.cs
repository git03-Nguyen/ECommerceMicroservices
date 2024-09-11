using Catalog.Service.Repositories;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

public class GetManyProductsHandler : IRequestHandler<GetManyProductsQuery, GetManyProductsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetManyProductsResponse> Handle(GetManyProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.ProductRepository.GetByCondition(p => request.Payload.Payload.Contains(p.ProductId));
        return new GetManyProductsResponse(products);
    }
}