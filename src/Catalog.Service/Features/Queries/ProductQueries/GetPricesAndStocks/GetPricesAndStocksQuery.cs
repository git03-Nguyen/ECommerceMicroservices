using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksQuery : IRequest<GetPricesAndStocksResponse>
{
    public GetPricesAndStocksQuery(GetPricesAndStocksRequest payload)
    {
        Payload = payload;
    }

    public GetPricesAndStocksRequest Payload { get; set; }
}