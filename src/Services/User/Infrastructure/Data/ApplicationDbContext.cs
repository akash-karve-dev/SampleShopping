using Microsoft.EntityFrameworkCore;
using User.Application.Data;

namespace User.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbConext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Domain.User> Users { get; set; }
    }
}