using Contracts.Constants;

namespace Order.Service.Options;

public class DatabaseOptions
{
    public const string Name = nameof(DatabaseOptions);

    public string ConnectionString { get; set; } = string.Empty;
    public string SchemaName { get; set; } = DatabaseConstants.DefaultSchemaName;
}