namespace Basket.Service.Options;

public class BasketDbOptions
{
    public const string Name = nameof(BasketDbOptions);

    public string ConnectionString { get; set; } = string.Empty;
}