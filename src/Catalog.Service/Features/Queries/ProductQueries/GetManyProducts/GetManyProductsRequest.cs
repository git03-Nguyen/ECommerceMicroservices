namespace Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;

public class GetManyProductsRequest
{
    public IEnumerable<int> Payload { get; set; }
}