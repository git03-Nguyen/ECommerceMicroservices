using Basket.Service.Data.DbContexts;
using Basket.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basket.Service.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly BasketDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(BasketDbContext context, IBasketRepository basketRepository,
        IBasketItemRepository basketItemRepository, IProductRepository productRepository,
        ISellerRepository sellerRepository)
    {
        _context = context;
        BasketRepository = basketRepository;
        BasketItemRepository = basketItemRepository;
        ProductRepository = productRepository;
        SellerRepository = sellerRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IBasketRepository BasketRepository { get; }
    public IBasketItemRepository BasketItemRepository { get; }
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