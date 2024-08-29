using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Data.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    
}