namespace Basket.Service.Options;

public class AuthOptions
{
    public const string Name = nameof(AuthOptions);
    
    public string Authority { get; set; } = string.Empty;
}