using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdResponse>
{
    public GetCategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; set; }
}