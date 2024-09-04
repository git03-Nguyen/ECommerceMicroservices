namespace Auth.Service.Exceptions;

public class ResourceAlreadyExistsException : Exception
{
    public string Key { get; set; }
    public string Value { get; set; }
    
    public ResourceAlreadyExistsException(string key, string value) : base($"Resource with {key}: {value} already exists")
    {
        Key = key;
        Value = value;
    }
    
}