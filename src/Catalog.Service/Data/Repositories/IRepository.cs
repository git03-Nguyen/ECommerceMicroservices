using System.Linq.Expressions;

namespace Catalog.Service.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetBy(Expression<Func<T, bool>> predicate);
    Task<T> Create(T newEntity);
    Task<T> Update(T newEntity);
    Task<bool> Delete(int id);
    Task<bool> Exists(Expression<Func<T, bool>> predicate);
    
}