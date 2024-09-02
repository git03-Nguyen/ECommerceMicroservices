using Microsoft.EntityFrameworkCore.Storage;
using Order.Service.Data.DbContexts;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly OrderDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(OrderDbContext context, IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository)
    {
        _context = context;
        OrderRepository = orderRepository;
        OrderItemRepository = orderItemRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IOrderRepository OrderRepository { get; }
    public IOrderItemRepository OrderItemRepository { get; }

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