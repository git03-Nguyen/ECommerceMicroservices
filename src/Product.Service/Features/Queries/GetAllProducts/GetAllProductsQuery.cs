using MediatR;
using Product.Service.Models;

namespace Product.Service.Features.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductItem>>
{
}