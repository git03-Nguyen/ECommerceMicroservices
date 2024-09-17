using Contracts.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Contracts.Middlewares;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);

        var entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Deleted);

        foreach (var softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.IsDeleted = true;
            softDeletable.Entity.DeletedAt = DateTime.UtcNow;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}