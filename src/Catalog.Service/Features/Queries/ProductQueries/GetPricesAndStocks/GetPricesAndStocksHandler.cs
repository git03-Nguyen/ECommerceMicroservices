using Catalog.Service.Models.Dtos;
using Catalog.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Queries.ProductQueries.GetPricesAndStocks;

public class GetPricesAndStocksHandler : IRequestHandler<GetPricesAndStocksQuery, GetPricesAndStocksResponse>
{
    private readonly ICatalogUnitOfWork _unitOfWork;

    public GetPricesAndStocksHandler(ICatalogUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPricesAndStocksResponse> Handle(GetPricesAndStocksQuery request,
        CancellationToken cancellationToken)
    {
        // Check if the product ids are valid
        var invalidProductIds = new List<int>();
        var validProductIds = new List<int>();

        foreach (var productId in request.Payload.ProductIds)
        {
            var exist = await _unitOfWork.ProductRepository.CheckExistsByConditionAsync(p => p.ProductId == productId);
            if (exist) validProductIds.Add(productId);
            else invalidProductIds.Add(productId);
        }

        var products = _unitOfWork.ProductRepository.GetPriceAndStock(validProductIds, cancellationToken);
        var productsDtos = await products.Select(p => new ProductPriceStockDto(p)).ToListAsync(cancellationToken);
        return new GetPricesAndStocksResponse
        {
            Payload = productsDtos,
            ErrorIds = invalidProductIds
        };
    }
}