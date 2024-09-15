using System.Text.RegularExpressions;

namespace Contracts.Helpers;

public class CustomKebabNameGenerator
{
    private static readonly string _separator = "-";
    private const string _sendPrefix = "send";
    private const string _receivePrefix = "";
    
    private static readonly Regex _pattern = new("(?<=[a-z0-9])[A-Z]", RegexOptions.Compiled);
    
    public string SantinizeSendingExchangeName(string name)
    {
        return SanitizeName(name, _sendPrefix);
    }
    
    public string SantinizeReceivingQueueName(string name)
    {
        return SanitizeName(name, _receivePrefix);
    }
    
    private string SanitizeName(string name, string prefix)
    {
        var result = new string(name);
        
        // 1. Remove the namespace
        do 
        {
            var index = result.IndexOf('.');
            if (index == -1) break;
            result = result.Substring(index + 1);
        } while (true);
        
        // 3. Remove the I if it is an interface
        if (result.StartsWith("I") && char.IsUpper(result[1]))
        {
            result = result.Substring(1);
        }
        
        // 3. Replace uppercase letters with a dash followed by the lowercase letter
        result = _pattern.Replace(result, m => _separator + m.Value).ToLowerInvariant();
        
        // 4. Add the prefix
        if (!string.IsNullOrEmpty(prefix))
        {
            result = $"{prefix}{_separator}{result}";
        }
        
        return result;
    }
    
}