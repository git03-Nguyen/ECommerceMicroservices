using Catalog.Service.Data.DbContexts;
using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Interfaces;

namespace Catalog.Service.Repositories.Implements;

public class SellerRepository : GenericRepository<Seller, CatalogDbContext>, ISellerRepository
{
    public SellerRepository(CatalogDbContext context) : base(context)
    {
    }
}