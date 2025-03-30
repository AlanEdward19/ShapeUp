using AuthService.Common.Interfaces;
using AuthService.Common.User;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.GetUsersFromGroup;

public class GetUsersFromGroupQueryHandler(IGroupRepository repository) : IHandler<ICollection<UserDto>, GetUsersFromGroupQuery>
{
    public async Task<ICollection<UserDto>> HandleAsync(GetUsersFromGroupQuery query, CancellationToken cancellationToken)
    {
        ICollection<User> users = await repository.GetUsersFromGroupAsync(query.GroupId, cancellationToken);
        
        return users
            .Select(user => new UserDto(user))
            .ToList();
    }
}