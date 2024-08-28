using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategories;

public class GetAllCategoriesResponse
{
    public IEnumerable<CategoryDto> Payload { get; set; }
    
    public GetAllCategoriesResponse(IEnumerable<Category> categories)
    {
        Payload = categories.Select(category => new CategoryDto(category));
    }
    
}