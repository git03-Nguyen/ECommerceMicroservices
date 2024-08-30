using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Repositories.Implements;

public abstract class GenericRepositoy<T, D> : IGenericRepository<T> where T : class
    where D : DbContext
{
    protected readonly D _context;
    protected readonly DbSet<T> _dbSet;
    
    public GenericRepositoy(D context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public DbSet<T> GetDbSet()
    {
        return _dbSet;
    }

    public async Task<T> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression);
    }

    public Task<bool> CheckExistsByConditionAsync(Expression<Func<T, bool>> expression)
    {
        return _dbSet.AnyAsync(expression);
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity, CancellationToken.None);
        return true;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entity)
    {
        await _dbSet.AddRangeAsync(entity);
        return true;
    }

    public bool Update(T entity)
    {
        _dbSet.Update(entity);
        return true;
    }

    public bool UpdateRange(List<T> entities)
    {
        _dbSet.UpdateRange(entities);
        return true;
    }

    // Usage: Remove an entity from the context
    public bool Remove(T entity)
    {
        _dbSet.Remove(entity);
        return true;
    }

    // Usage: Remove a range of entities from the context
    public bool RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return true;
    }

    // Usage: (!) Delete a range of entities from the database
    public async Task<int> DeleteRangeAsync(Expression<Func<T, bool>> expression)
    {
        var entities = _dbSet.Where(expression);
        _dbSet.RemoveRange(entities);
        return await _context.SaveChangesAsync();
    }
}