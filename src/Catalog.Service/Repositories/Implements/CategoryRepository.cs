using Catalog.Service.Data.DbContexts;
using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Interfaces;

namespace Catalog.Service.Repositories.Implements;

public class CategoryRepository : GenericRepositoy<Category, CatalogDbContext>, ICategoryRepository
{
    public CategoryRepository(CatalogDbContext context) : base(context)
    {
    }
}