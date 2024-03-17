using Microsoft.EntityFrameworkCore;

namespace User.Application.Data
{
    public interface IApplicationDbConext
    {
        DbSet<Domain.User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}