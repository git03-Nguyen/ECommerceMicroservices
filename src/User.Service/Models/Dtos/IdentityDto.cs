namespace User.Service.Models.Dtos;

public class IdentityDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string AccessToken { get; set; }
    public string ExpiresIn { get; set; }
}