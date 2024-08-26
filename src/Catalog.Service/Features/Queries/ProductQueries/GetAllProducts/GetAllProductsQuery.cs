using Catalog.Service.Data.Models;
using MediatR;

namespace Catalog.Service.Features.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
    
    
}