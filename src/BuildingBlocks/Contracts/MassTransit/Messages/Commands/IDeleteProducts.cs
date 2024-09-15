namespace Contracts.MassTransit.Messages.Commands;

public interface IDeleteProducts
{
    public IEnumerable<int> ProductIds { get; set; }
}