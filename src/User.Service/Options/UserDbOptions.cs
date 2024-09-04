namespace User.Service.Options;

public class UserDbOptions
{
    public const string Name = nameof(UserDbOptions);

    public string ConnectionString { get; set; } = string.Empty;
}