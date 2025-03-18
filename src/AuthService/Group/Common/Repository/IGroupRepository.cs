using AuthService.Common.User;
using AuthService.Group.Common.Enums;

namespace AuthService.Group.Common.Repository;

public interface IGroupRepository
{
    Task AddUserToGroupAsync(Guid groupId, Guid userId, EGroupRole role,
        CancellationToken cancellationToken);

    Task RemoveUserFromGroupAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);

    Task AddAsync(Group group, CancellationToken cancellationToken);

    Task DeleteAsync(Guid groupId, CancellationToken cancellationToken);

    Task<ICollection<User>> GetUsersFromGroupAsync(Guid groupId, CancellationToken cancellationToken);
}