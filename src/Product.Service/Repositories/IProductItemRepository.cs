using System.Linq.Expressions;

namespace Product.Service.Repositories;

public interface IProductItemRepository
{
    Task<IEnumerable<Models.ProductItem>> GetAll();
    Task<Models.ProductItem?> GetBy(Expression<Func<Models.ProductItem, bool>> predicate);
    Task<Models.ProductItem?> Create(Models.ProductItem productItem);
    Task<Models.ProductItem?> Update(Models.ProductItem productItem);
    Task<bool> Delete(Guid id);
    Task<bool> Exists(Expression<Func<Models.ProductItem, bool>> predicate);
}