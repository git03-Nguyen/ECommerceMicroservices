namespace User.Service.Data.Models;

public class Account
{
    public Guid AccountId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}