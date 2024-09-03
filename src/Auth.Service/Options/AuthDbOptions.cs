namespace Auth.Service.Options;

public class AuthDbOptions
{
    public const string Name = nameof(AuthDbOptions);
    public string ConnectionString { get; set; } = string.Empty;
}