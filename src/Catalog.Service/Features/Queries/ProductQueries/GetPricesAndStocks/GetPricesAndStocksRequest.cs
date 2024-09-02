namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksRequest
{
    public IEnumerable<int> ProductIds { get; set; }
}