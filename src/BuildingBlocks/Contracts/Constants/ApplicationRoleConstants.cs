namespace Contracts.Constants;

public class ApplicationRoleConstants
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";
    public const string Seller = "Seller";
    
    public static IEnumerable<string> AllRoles => new List<string> { Admin, Customer, Seller };
    
    // TODO: best practice is to use bitwise operations for roles
}