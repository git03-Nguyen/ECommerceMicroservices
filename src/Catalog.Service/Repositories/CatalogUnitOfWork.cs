using System.Data;
using Catalog.Service.Data.DbContexts;
using Catalog.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Service.Repositories;

public class CatalogUnitOfWork : ICatalogUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly CatalogDbContext _context;
    private IDbContextTransaction _transaction;
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    
    public CatalogUnitOfWork(CatalogDbContext context, ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _context = context;
        CategoryRepository = categoryRepository;
        ProductRepository = productRepository;
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    { 
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _transaction.CommitAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _transaction.RollbackAsync(cancellationToken);
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