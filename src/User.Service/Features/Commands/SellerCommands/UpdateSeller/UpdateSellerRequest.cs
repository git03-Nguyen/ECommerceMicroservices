namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerRequest
{
    public int? Id { get; set; }
    public Guid AccountId { get; set; }
    
    public string? Email { get; set; }
    public string? UserName { get; set; } 
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; } 
    public string? PaymentMethod { get; set; }
}