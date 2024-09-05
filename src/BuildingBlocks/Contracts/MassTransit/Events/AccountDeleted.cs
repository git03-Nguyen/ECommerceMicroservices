namespace Contracts.MassTransit.Events;

public class AccountDeleted
{
    public Guid AccountId { get; set; }
    public string Role { get; set; }
}