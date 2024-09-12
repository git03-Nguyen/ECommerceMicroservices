namespace Contracts.Models;

public class IdentityDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Role { get; set; }
}