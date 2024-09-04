namespace User.Service.Data.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    
    // TODO: Add PaymentMethod property
    public string PaymentMethod { get; set; } = string.Empty;
    
}