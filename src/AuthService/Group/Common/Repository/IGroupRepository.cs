using AuthService.Common.User;
using AuthService.Group.Common.Enums;

namespace AuthService.Group.Common.Repository;

public interface IGroupRepository
{
    Task AddUserToGroupAsync(Guid groupId, string userId, EGroupRole role,
        CancellationToken cancellationToken);
    
    Task ChangeUserRoleInGroupAsync(Guid groupId, string userId, EGroupRole role,
        CancellationToken cancellationToken);

    Task RemoveUserFromGroupAsync(Guid groupId, string userId, CancellationToken cancellationToken);

    Task AddAsync(Group group, string userId, CancellationToken cancellationToken);

    Task DeleteAsync(Guid groupId, CancellationToken cancellationToken);

    Task<ICollection<User>> GetUsersFromGroupAsync(Guid groupId, CancellationToken cancellationToken);
}