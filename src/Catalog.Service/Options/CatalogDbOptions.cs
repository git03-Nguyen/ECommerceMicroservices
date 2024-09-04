namespace Catalog.Service.Options;

public class CatalogDbOptions
{
    public const string Name = nameof(CatalogDbOptions);
    
    public string ConnectionString { get; set; }
}