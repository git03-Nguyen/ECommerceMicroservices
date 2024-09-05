namespace Contracts.MassTransit.Events;

public class NewAccountCreated
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}