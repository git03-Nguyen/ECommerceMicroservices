using Catalog.Service.Data.Models;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdResponse>
{
    public int CategoryId { get; set; }

    public GetCategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}