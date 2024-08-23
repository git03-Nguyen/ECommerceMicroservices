using Microsoft.EntityFrameworkCore;

namespace AuthService.Data;

public class AccountContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AccountContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}