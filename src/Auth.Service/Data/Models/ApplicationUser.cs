using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Data.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; }
}