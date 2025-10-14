using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Domain.Entities;
using Orderly.Catalog.Entities;

namespace Orderly.Catalog.Database
{
    public class CatalogDbContext:DbContext
    {
         public DbSet<Product> Products { get; set; }
         public DbSet<Vendor> Vendors { get; set; }
         public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                
                builder.HasKey(p => p.Id);
                builder.Property(p=> p.Id).ValueGeneratedOnAdd();
                builder.Property(p => p.SKU).IsRequired().HasMaxLength(50);
                builder.HasOne(p => p.Vendor)
                    .WithMany(v => v.Products)
                    .HasForeignKey(p => p.VendorId);

                builder.HasIndex(p => new { p.SKU, p.VendorId }).IsUnique();
                builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
                builder.Property(p => p.Description).HasMaxLength(500);
                builder.Property(p => p.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Vendor>(builder =>
            {
                builder.HasKey(v => v.Id);
                builder.Property(v => v.Id).ValueGeneratedOnAdd();
                builder.HasIndex(v=> v.Name).IsUnique();
            });
        }
    }
}
