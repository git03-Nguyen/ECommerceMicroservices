using Basket.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basket.Service.Repositories;

public interface IUnitOfWork
{
    public IBasketRepository BasketRepository { get; }
    public IBasketItemRepository BasketItemRepository { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

}