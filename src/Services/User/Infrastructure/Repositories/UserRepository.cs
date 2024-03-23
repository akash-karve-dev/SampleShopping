using Microsoft.EntityFrameworkCore;
using User.Domain.User;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddUserAsync(Domain.User.User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<Domain.User.User?> GetByIdAync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync([id], cancellationToken: cancellationToken);
        }

        public Task<bool> IsUserExistAsync(string name, string email, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.AnyAsync(u => u.Name == name && u.Email == email,
             cancellationToken);
        }
    }
}