namespace User.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserRequest
{
    public required Guid UserId { get; set; }

    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }

    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PaymentDetails { get; set; }
}