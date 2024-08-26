using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProductById;

public class GetProductByIdQuery : IRequest<Product>
{
    public GetProductByIdRequest Payload { get; }

    public GetProductByIdQuery(GetProductByIdRequest request)
    {
        Payload = request;
    }
}