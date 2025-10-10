using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Entities;

namespace Orderly.Catalog.Database
{
    public class CatalogDbContext:DbContext
    {
         public DbSet<Product> Products { get; set; }
         public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    }
}
