namespace Auth.Service.Exceptions;

public class ResourceNotFoundException : Exception
{
    public string Key { get; set; }
    public string Value { get; set; }
    
    public ResourceNotFoundException(string key, string value) : base($"Resource with {key}: {value} not found")
    {
        Key = key;
        Value = value;
    }
    
}