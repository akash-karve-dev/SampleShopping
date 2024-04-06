using Microsoft.EntityFrameworkCore;

namespace OrderSaga.Worker
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}