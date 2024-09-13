namespace Contracts.MassTransit.Messages.Events;

public class AccountCreated
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    // public string? Avatar { get; set; }
    // public bool IsOwnImage { get; set; }
}