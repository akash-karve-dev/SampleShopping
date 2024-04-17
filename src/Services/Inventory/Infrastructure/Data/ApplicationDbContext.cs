using Inventory.Application.Data;
using Inventory.Domain.Product;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Data
{
    internal class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        public DbSet<Domain.Inventory.Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddInboxStateEntity();

            modelBuilder.Entity<Product>()
                        .Property(x => x.ProductCategory)
                        .HasColumnName("Category");

            base.OnModelCreating(modelBuilder);
        }
    }
}