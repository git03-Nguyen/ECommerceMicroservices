namespace Contracts.MassTransit.Messages.Events.Account.AccountCreated;

public interface ICustomerCreated
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    public string Address { get; set; }
}