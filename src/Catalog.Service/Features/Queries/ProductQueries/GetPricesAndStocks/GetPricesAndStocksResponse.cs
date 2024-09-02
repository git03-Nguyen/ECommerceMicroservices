using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksResponse
{
    public List<ProductPriceStockDto> Payload { get; set; } = new();

    public List<int> ErrorIds { get; set; } = new();
}