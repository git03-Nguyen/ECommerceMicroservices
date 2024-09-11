using Microsoft.EntityFrameworkCore.Storage;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}