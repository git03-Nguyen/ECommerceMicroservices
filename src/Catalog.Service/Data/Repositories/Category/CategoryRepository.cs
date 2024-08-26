using System.Linq.Expressions;
using Catalog.Service.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _context;

    public CategoryRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Category>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Models.Category> GetBy(Expression<Func<Models.Category, bool>> predicate)
    {
        return await _context.Categories.FirstOrDefaultAsync(predicate!);
    }

    public async Task<Models.Category> Create(Models.Category newEntity)
    {
        var entity = await _context.Categories.AddAsync(newEntity);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Models.Category> Update(Models.Category newEntity)
    {
        var entity = _context.Categories.Update(newEntity);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(Expression<Func<Models.Category, bool>> predicate)
    {
        return await _context.Categories.AnyAsync(predicate!);
    }
}