using Microsoft.EntityFrameworkCore.Storage;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories;

public interface IUnitOfWork
{
    public ICustomerRepository CustomerRepository { get; }
    public ISellerRepository SellerRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}