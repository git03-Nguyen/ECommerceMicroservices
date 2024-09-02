namespace ApiGateway.Options;

public class OcelotOptions
{
    public const string Name = "Routes";

    public string Folder { get; set; } = string.Empty;
    public string FileOfSwaggerEndPoints { get; set; } = string.Empty;
}