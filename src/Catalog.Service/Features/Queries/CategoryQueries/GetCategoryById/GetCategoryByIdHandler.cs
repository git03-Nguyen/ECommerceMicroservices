using Catalog.Service.Repositories;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    private readonly ICatalogUnitOfWork _catalogUnitOfWork;

    public GetCategoryByIdHandler(ICatalogUnitOfWork catalogUnitOfWork)
    {
        _catalogUnitOfWork = catalogUnitOfWork;
    }


    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _catalogUnitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null) throw new CategoryNotFoundException(request.CategoryId);

        return new GetCategoryByIdResponse(category);
    }
}

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException(int requestId)
    {
        RequestId = requestId;
    }

    public int RequestId { get; }

    public override string Message => $"Category with id {RequestId} not found";
}