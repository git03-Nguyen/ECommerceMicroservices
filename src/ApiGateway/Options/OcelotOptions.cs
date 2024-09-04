namespace ApiGateway.Options;

public class OcelotOptions
{
    public const string Name = nameof(OcelotOptions);

    public string Folder { get; set; } = string.Empty;
}