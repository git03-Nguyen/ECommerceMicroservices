using Microsoft.EntityFrameworkCore.Storage;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Repositories;

public interface IUnitOfWork
{
    public IOrderRepository OrderRepository { get; }
    public IOrderItemRepository OrderItemRepository { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

}