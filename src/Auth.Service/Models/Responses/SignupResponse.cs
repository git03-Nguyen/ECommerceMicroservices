namespace Auth.Service.Models.Responses;

public class SignupResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}