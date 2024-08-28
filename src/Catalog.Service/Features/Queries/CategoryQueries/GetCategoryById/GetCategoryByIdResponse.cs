using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdResponse
{
    public CategoryDto Payload { get; set; }
    
    public GetCategoryByIdResponse(Category category)
    {
        Payload = new CategoryDto(category);
    }
}