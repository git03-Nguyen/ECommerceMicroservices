using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<Category>
{
    public GetCategoryByIdRequest Payload { get; }

    public GetCategoryByIdQuery(GetCategoryByIdRequest request)
    {
        Payload = request;
    }
}