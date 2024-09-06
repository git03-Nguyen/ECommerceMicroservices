namespace User.Service.Models.Dtos;

public class CustomerDto
{
    public int Id { get; set; }

    public Guid AccountId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;
}