using Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Data.Models;

public class ApplicationUser : IdentityUser<Guid>, ISoftDelete
{
    public string FullName { get; set; }
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