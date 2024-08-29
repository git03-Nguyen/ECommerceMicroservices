using System.ComponentModel.DataAnnotations.Schema;
using Auth.Service.Shared;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Data.Models;

public class User : IdentityUser
{
    public override string Id { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime? CreatedDate { get; set; }  
    public DateTime? ModifiedDate { get; set; }
    
}