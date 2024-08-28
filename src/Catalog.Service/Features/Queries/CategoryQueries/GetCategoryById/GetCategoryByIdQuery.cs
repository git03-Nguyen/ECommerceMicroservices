using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdResponse>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}