using Microsoft.EntityFrameworkCore.Storage;
using User.Service.Data.DbContexts;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly UserDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(UserDbContext context,
        IUserRepository userRepository)
    {
        _context = context;
        UserRepository = userRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IUserRepository UserRepository { get; }

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