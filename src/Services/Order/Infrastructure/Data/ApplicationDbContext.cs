using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Application.Data;
using Order.Domain.Order;

namespace Order.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Order.Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * When we do not follow EF convention for relationship, we need to configure it with fluent methods
             */

            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddInboxStateEntity();

            modelBuilder.Entity<Domain.Order.Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne()
                .HasForeignKey(o => o.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}