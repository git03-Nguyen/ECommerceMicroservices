using User.Service.Data.DbContexts;
using User.Service.Data.Models;
using User.Service.Repositories.Interfaces;

namespace User.Service.Repositories.Implements;

public class SellerRepository : GenericRepositoy<Seller, UserDbContext>, ISellerRepository
{
    public SellerRepository(UserDbContext context) : base(context)
    {
    }
}