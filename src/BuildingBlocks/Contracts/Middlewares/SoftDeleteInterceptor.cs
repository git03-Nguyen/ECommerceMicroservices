using Contracts.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Contracts.Middlewares;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    // public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    // {
    //     if (eventData.Context is null) return result;
    //
    //     foreach (var entry in eventData.Context.ChangeTracker.Entries())
    //     {
    //         if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;
    //         entry.State = EntityState.Modified;
    //         delete.Delete();
    //     }
    //
    //     return result;
    // }

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