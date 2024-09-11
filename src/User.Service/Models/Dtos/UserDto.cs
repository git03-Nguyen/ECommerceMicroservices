using User.Service.Data.Models;

namespace User.Service.Models.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    
    public string? Role { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    
    public string PaymentDetails { get; set; }
    
    public UserDto(Data.Models.User user)
    {
        UserId = user.UserId;
        Role = user.Role;
        UserName = user.UserName;
        Email = user.Email;
        FullName = user.FullName;
        PhoneNumber = user.PhoneNumber;
        Address = user.Address;
        PaymentDetails = user.PaymentDetails;
    }
    
}