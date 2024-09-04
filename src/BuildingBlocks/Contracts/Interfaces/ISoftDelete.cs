namespace Contracts.Interfaces;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public virtual void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public virtual void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}