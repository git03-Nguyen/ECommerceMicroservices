using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
{
    public GetAllCategoriesRequest Payload { get; }
    
    public GetAllCategoriesQuery(GetAllCategoriesRequest request)
    {
        Payload = request;
    }
    
}