using System.Linq.Expressions;
using Product.Service.Models;

namespace Product.Service.Repositories;

public interface IProductItemRepository
{
    Task<IEnumerable<ProductItem>> GetAll();
    Task<ProductItem?> GetBy(Expression<Func<ProductItem, bool>> predicate);
    Task<ProductItem?> Create(ProductItem productItem);
    Task<ProductItem?> Update(ProductItem productItem);
    Task<bool> Delete(Guid id);
    Task<bool> Exists(Expression<Func<ProductItem, bool>> predicate);
}