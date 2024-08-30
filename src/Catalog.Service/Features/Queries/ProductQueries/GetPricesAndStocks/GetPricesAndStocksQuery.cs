using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksQuery : IRequest<GetPricesAndStocksResponse>
{
    public GetPricesAndStocksRequest Payload { get; set; }
    
    public GetPricesAndStocksQuery(GetPricesAndStocksRequest payload)
    {
        Payload = payload;
    }
    
}