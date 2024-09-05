namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerRequest
{
    public required int Id { get; set; }
    
    public string? Email { get; set; }
    public string? Username { get; set; } 
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; } 
    public string? PaymentMethod { get; set; }
}