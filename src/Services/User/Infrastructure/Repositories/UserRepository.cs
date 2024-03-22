using User.Domain.User;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<Domain.User.User?> GetByIdAync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync(id, cancellationToken);
        }
    }
}