using System.Linq.Expressions;
using Catalog.Service.Data.DbContexts;
using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Repositories.Implements;

public class ProductRepository : GenericRepositoy<Product, CatalogDbContext>, IProductRepository
{
    public ProductRepository(CatalogDbContext context) : base(context)
    {
    }
}