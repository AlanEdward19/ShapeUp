namespace AuthService.Common.User.Repository;

public interface IUserRepository
{
    Task<User?> GetByObjectIdAsync(string objectId, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);

    Task<User> GetUserAsync(string userId,
        CancellationToken cancellationToken);
}