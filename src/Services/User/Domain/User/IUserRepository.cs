namespace User.Domain.User
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAync(Guid id, CancellationToken cancellationToken);

        Task AddUserAsync(User user, CancellationToken cancellationToken);
    }
}