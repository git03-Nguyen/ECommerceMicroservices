using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

public class GetManyProductsQuery : IRequest<GetManyProductsResponse>
{
    public GetManyProductsQuery(GetManyProductsRequest request)
    {
        Payload = request;
    }

    public GetManyProductsRequest Payload { get; set; }
}