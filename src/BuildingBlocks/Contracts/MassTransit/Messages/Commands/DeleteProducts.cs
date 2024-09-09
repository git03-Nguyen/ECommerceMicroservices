namespace Contracts.MassTransit.Messages.Commands;

public class DeleteProducts
{
    public IEnumerable<int> ProductIds { get; set; }
}