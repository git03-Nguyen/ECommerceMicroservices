using Catalog.Service.Data.Models;
using Catalog.Service.Data.Repositories.Category;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _categoryRepository.GetBy(p => p.CategoryId == request.Payload.Id);
        if (product == null) throw new CategoryNotFoundException(nameof(Category), request.Payload.Id);
            
        return product;
    }
}

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(string categoryName, int requestId)
    {
        CategoryName = categoryName;
        RequestId = requestId;
    }
    
    public string CategoryName { get; }
    public int RequestId { get; }
    
    public override string Message => $"Category {CategoryName} with id {RequestId} not found";
    
}