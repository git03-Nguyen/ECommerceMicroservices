using Catalog.Service.Data.Models;
using Catalog.Service.Models.Filters;
using Catalog.Service.Repositories;
using Contracts.Constants;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Queries.ProductQueries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public GetProductsHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var requestPayload = request.Payload;

        // Filter by category-id, price range, search term
        var categoryIds = new List<int>();
        if (!string.IsNullOrEmpty(requestPayload.CategoryIds))
            categoryIds = requestPayload.CategoryIds.Split(',').Select(int.Parse).ToList();
        requestPayload.SearchTerm = requestPayload.SearchTerm.Trim().ToLower();

        // Check role: Customer and Admin can see all products, Seller can see only their products
        var user = _identityService.GetUserInfoIdentity();
        var userId = Guid.Parse(user.Id);
        IQueryable<Product> products;
        if (user.Role == ApplicationRoleConstants.Seller)
        {
            products = _unitOfWork.ProductRepository.GetByCondition(x =>
                (x.SellerId == userId) &&
                (categoryIds.Count == 0 || categoryIds.Contains(x.CategoryId)) &&
                (x.Price >= requestPayload.MinPrice) &&
                (x.Price <= requestPayload.MaxPrice) &&
                (x.Name.Contains(requestPayload.SearchTerm)) // should use normalized text
            );
        }
        else
        {
            products = _unitOfWork.ProductRepository.GetByCondition(x =>
                (categoryIds.Count == 0 || categoryIds.Contains(x.CategoryId)) &&
                (x.Price >= requestPayload.MinPrice) &&
                (x.Price <= requestPayload.MaxPrice) &&
                (x.Name.Contains(requestPayload.SearchTerm))
            );
        }

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

        return new GetProductsResponse(products, totalPage, requestPayload.PageNumber, requestPayload.PageSize,
            totalProductSize);
    }
}