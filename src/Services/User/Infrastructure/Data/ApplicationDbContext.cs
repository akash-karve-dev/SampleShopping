using MassTransit;
using Microsoft.EntityFrameworkCore;
using User.Application.Abstractions;

namespace User.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.User.User>()
                        .ToTable(nameof(Users), t => t.ExcludeFromMigrations());

            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public DbSet<Domain.User.User> Users { get; set; }
    }
}