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
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public DbSet<Domain.User.User> Users { get; set; }
    }
}