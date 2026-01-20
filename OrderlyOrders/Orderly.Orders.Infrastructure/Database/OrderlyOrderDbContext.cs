using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Infrastructure.Database
{
    public class OrderlyOrderDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderlyOrderDbContext(DbContextOptions<OrderlyOrderDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(x =>
            {
                x.HasKey(o => o.Id);
                x.Property(o => o.Id).ValueGeneratedOnAdd();
                x.HasMany(o => o.Items).WithOne(ot => ot.Order);
            });

            modelBuilder.Entity<OrderItem>(x =>
            {

                x.HasKey(ot => ot.Id);
                x.Property(ot => ot.Id).ValueGeneratedOnAdd();
                x.HasIndex(ot => new { ot.OrderId, ot.ProductId }).IsUnique();
                x.HasOne(ot => ot.Order).WithMany(ot => ot.Items);

            });
        }
    }
}
