namespace Contracts.MassTransit.Messages.Events;

public interface IAccountDeleted
{
    public Guid AccountId { get; set; }
    public string Role { get; set; }
}