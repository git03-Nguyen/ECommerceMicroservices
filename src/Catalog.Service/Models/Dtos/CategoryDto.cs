using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Dtos;

public class CategoryDto
{
    // Limit the properties to be returned
    public CategoryDto(Category category)
    {
        CategoryId = category.CategoryId;
        Name = category.Name;
        Description = category.Description;
        ImageUrl = category.ImageUrl;
    }

    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}