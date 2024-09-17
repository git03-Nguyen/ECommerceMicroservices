using Catalog.Service.Data.DbContexts;
using Catalog.Service.Repositories.Implements;
using Catalog.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Service.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly CatalogDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(CatalogDbContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
        SellerRepository = new SellerRepository(_context);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ISellerRepository SellerRepository { get; }

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
}