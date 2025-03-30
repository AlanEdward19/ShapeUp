namespace AuthService.Common.User.Repository;

public interface IUserRepository
{
    Task<User?> GetByObjectIdAsync(Guid objectId, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
}