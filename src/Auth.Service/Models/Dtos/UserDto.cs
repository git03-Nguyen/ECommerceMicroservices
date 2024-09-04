namespace Auth.Service.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public IEnumerable<string> Roles { get; set; }
}