namespace Order.Service.Options;

public class OrderDbOptions
{
    public const string Name = nameof(OrderDbOptions);

    public string ConnectionString { get; set; } = string.Empty;
}