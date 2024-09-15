namespace Contracts.MassTransit.Messages.Events.Account.AccountDeleted;

public interface IAccountDeleted
{
    public Guid AccountId { get; set; }
    public string Role { get; set; }
}