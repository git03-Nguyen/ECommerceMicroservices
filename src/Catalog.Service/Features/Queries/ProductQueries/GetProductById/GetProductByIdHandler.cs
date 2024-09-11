using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (product == null) throw new ResourceNotFoundException(nameof(Product), request.ProductId.ToString());

        return new GetProductByIdResponse(product);
    }
}
