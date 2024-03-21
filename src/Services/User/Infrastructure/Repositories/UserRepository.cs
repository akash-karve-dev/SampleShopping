using User.Application.Data;
using User.Domain.User;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repositories
{
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly ApplicationDbContext _dbContext;

    //    public UnitOfWork(ApplicationDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //    {
    //        return _dbContext.SaveChangesAsync(cancellationToken);
    //    }
    //}

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(Domain.User.User user, CancellationToken cancellationToken)
        {
            try
            {
                await Task.CompletedTask;
                var entity = _dbContext.Users.Add(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Domain.User.User?> GetByIdAync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync(id, cancellationToken);
        }
    }
}