using Catalog.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Service.Repositories;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ISellerRepository SellerRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}