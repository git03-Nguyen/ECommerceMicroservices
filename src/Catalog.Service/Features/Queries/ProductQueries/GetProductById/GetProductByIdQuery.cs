using Catalog.Service.Data.Models;
using Catalog.Service.Models.Dtos;
using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}