using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var requestPayload = request.Payload;

        // Filter by category-id, price range
        var categoryIds = new List<int>();
        if (!string.IsNullOrEmpty(requestPayload.CategoryIds))
        {
            categoryIds = requestPayload.CategoryIds.Split(',').Select(int.Parse).ToList();
        }

        var products = _unitOfWork.ProductRepository.GetByCondition(x =>
            (categoryIds.Count == 0 || categoryIds.Contains(x.CategoryId)) &&
            (requestPayload.MinPrice == null || x.Price >= requestPayload.MinPrice) &&
            (requestPayload.MaxPrice == null || x.Price <= requestPayload.MaxPrice)
        );

        // Sort by name, price
        products = requestPayload.SortBy switch
        {
            nameof(Product.ProductId) => requestPayload.SortOrder == FilterConstants.Ascending
                ? products.OrderBy(x => x.ProductId)
                : products.OrderByDescending(x => x.ProductId),
            nameof(Product.Name) => requestPayload.SortOrder == FilterConstants.Ascending
                ? products.OrderBy(x => x.Name)
                : products.OrderByDescending(x => x.Name),
            nameof(Product.Price) => requestPayload.SortOrder == FilterConstants.Ascending
                ? products.OrderBy(x => x.Price)
                : products.OrderByDescending(x => x.Price),
            // Default sort by Id
            _ => products.OrderBy(x => x.ProductId)
        };

        // Paging
        var totalProductSize = await products.CountAsync(cancellationToken);
        var totalPage = totalProductSize / requestPayload.PageSize;
        totalPage += totalProductSize % requestPayload.PageSize > 0 ? 1 : 0;
        products = products.Skip((requestPayload.PageNumber - 1) * requestPayload.PageSize)
            .Take(requestPayload.PageSize);

        return new GetProductsResponse(products, totalPage, requestPayload.PageNumber, requestPayload.PageSize, totalProductSize);
    }
}