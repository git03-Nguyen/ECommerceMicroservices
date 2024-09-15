namespace Contracts.MassTransit.Messages.Events;

public interface IUserInfoUpdated
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PaymentMethod { get; set; }
    public string Role { get; set; }
}