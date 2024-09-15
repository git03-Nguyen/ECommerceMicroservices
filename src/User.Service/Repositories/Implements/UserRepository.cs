using User.Service.Data.DbContexts;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories.Implements;

public class UserRepository : GenericRepositoy<Data.Models.User, UserDbContext>, IUserRepository
{
    public UserRepository(UserDbContext context) : base(context)
    {
    }
}