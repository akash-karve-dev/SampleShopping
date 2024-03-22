using Microsoft.EntityFrameworkCore;
using User.Application.Abstractions;

namespace User.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Domain.User.User> Users { get; set; }
    }
}