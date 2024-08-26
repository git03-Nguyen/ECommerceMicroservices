using Catalog.Service.Data.Models;
using Catalog.Service.Data.Repositories.Category;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement logic with request.Payload
        return await _categoryRepository.GetAll();
    }
}