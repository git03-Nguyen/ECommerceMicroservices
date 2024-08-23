using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product.Service.Data;
using Product.Service.Models;

namespace Product.Service.Repositories;

public class ProductItemRepository : IProductItemRepository
{
    private readonly ProductItemContext _context;

    public ProductItemRepository(ProductItemContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductItem>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductItem?> GetBy(Expression<Func<ProductItem, bool>> predicate)
    {
        return await _context.Products.FirstOrDefaultAsync(predicate);
    }

    public async Task<ProductItem?> Create(ProductItem productItem)
    {
        await _context.Products.AddAsync(productItem);
        await _context.SaveChangesAsync();
        return productItem;
    }

    public async Task<ProductItem?> Update(ProductItem productItem)
    {
        _context.Products.Update(productItem);
        await _context.SaveChangesAsync();
        return productItem;
    }

    public async Task<bool> Delete(Guid id)
    {
        var productItem = await _context.Products.FindAsync(id);
        if (productItem == null)
        {
            return false;
        }

        _context.Products.Remove(productItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(Expression<Func<ProductItem, bool>> predicate)
    {
        return await _context.Products.AnyAsync(predicate);
    }
}