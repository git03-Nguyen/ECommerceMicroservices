using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    private readonly ICatalogUnitOfWork _catalogUnitOfWork;

    public GetCategoryByIdHandler(ICatalogUnitOfWork catalogUnitOfWork)
    {
        _catalogUnitOfWork = catalogUnitOfWork;
    }


    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _catalogUnitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null) throw new ResourceNotFoundException(nameof(Category), request.CategoryId.ToString());

        return new GetCategoryByIdResponse(category);
    }
}
