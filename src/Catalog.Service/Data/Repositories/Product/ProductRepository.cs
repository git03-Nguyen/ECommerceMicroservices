using System.Linq.Expressions;
using Catalog.Service.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.Repositories.Product;

public class ProductRepository : IProductRepository, IAsyncDisposable
{
    private readonly CatalogDbContext _context;

    public ProductRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Models.Product> GetBy(Expression<Func<Models.Product, bool>> predicate)
    {
        return await _context.Products.FirstOrDefaultAsync(predicate!);
    }

    public async Task<Models.Product> Create(Models.Product newEntity)
    {
        var entity = await _context.Products.AddAsync(newEntity);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Models.Product> Update(Models.Product newEntity)
    {
        var entity = _context.Products.Update(newEntity);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(Expression<Func<Models.Product, bool>> predicate)
    {
        return await _context.Products.AnyAsync(predicate!);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}