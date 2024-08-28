using Catalog.Service.Data.Models;

namespace Catalog.Service.Models.Requests;

public class CreateNewCategoryRequest
{
    public string Name { get; set; }    
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
}