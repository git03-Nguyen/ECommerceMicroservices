using Catalog.Service.Data.Repositories.Category;
using Catalog.Service.Data.Repositories.Product;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.CreateNewCategory;

public class CreateNewCategoryHandler : IRequestHandler<CreateNewCategoryCommand, CreateNewCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateNewCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateNewCategoryResponse> Handle(CreateNewCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Data.Models.Category
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description
        };
        
        var createdCategory = await _categoryRepository.Create(category);
        
        var newCategoryResponse = new CreateNewCategoryResponse
        {
            CategoryId = createdCategory.CategoryId,
            Name = createdCategory.Name
        };
        
        return newCategoryResponse;
        
    }
}