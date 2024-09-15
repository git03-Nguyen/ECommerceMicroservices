using Catalog.Service.Data.DbContexts;
using Catalog.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Service.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly CatalogDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(CatalogDbContext context, ICategoryRepository categoryRepository,
        IProductRepository productRepository, ISellerRepository sellerRepository)
    {
        _context = context;
        CategoryRepository = categoryRepository;
        ProductRepository = productRepository;
        SellerRepository = sellerRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
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

    public void Dispose()
    {
        _context.Dispose();
    }
}