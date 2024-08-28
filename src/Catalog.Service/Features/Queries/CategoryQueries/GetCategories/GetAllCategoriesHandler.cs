using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    private readonly ICatalogUnitOfWork _catalogUnitOfWork;

    public GetAllCategoriesHandler(ICatalogUnitOfWork catalogUnitOfWork)
    {
        _catalogUnitOfWork = catalogUnitOfWork;
    }

    public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _catalogUnitOfWork.CategoryRepository.GetAll().OrderBy(c => c.CategoryId).ToListAsync(cancellationToken);
        return new GetAllCategoriesResponse(categories);
    }
}