using Contracts.Models.Interfaces;

namespace User.Service.Data.Models;

public class User : ISoftDelete
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;

    // TODO: Add Payment's properties
    public string PaymentDetails { get; set; } = string.Empty;

    // Snapshot from Auth.Service
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Soft delete
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public virtual void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
        Email = Email + "_deleted_" + DeletedAt.Value.ToString("yyyyMMddHHmmss");
        UserName = UserName + "_deleted_" + DeletedAt.Value.ToString("yyyyMMddHHmmss");
    }

    public virtual void Restore()
    {
        Email = Email.Replace("_deleted_" + DeletedAt.Value.ToString("yyyyMMddHHmmss"), "");
        UserName = UserName.Replace("_deleted_" + DeletedAt.Value.ToString("yyyyMMddHHmmss"), "");
        IsDeleted = false;
        DeletedAt = null;
    }
}