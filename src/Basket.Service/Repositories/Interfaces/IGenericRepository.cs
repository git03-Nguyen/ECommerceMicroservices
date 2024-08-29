using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Repositories.Interfaces;

public interface IGenericRepository<T>  
    where T : class
{
    IQueryable<T> GetAll();
    DbSet<T> GetDbSet();
    Task<T> GetByIdAsync(object id);
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<bool> CheckExistsByConditionAsync(Expression<Func<T, bool>> expression);
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entity);
    bool Update(T entity);
    bool UpdateRange(List<T> entities);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entities);
    Task<int> DeleteRangeAsync(Expression<Func<T, bool>> expression);
    
}