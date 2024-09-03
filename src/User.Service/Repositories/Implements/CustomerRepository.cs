using User.Service.Data.DbContexts;
using User.Service.Data.Models;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories.Implements;

public class CustomerRepository : GenericRepositoy<Customer, UserDbContext>, ICustomerRepository
{
    public CustomerRepository(UserDbContext context) : base(context)
    {
    }
}