using AuthService.Common.Interfaces;
using AuthService.Common.User.Repository;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.GetUserPermissions;

public class GetUserPermissionsQueryHandler(IUserRepository repository) : IHandler<UserPermissionsDto, GetUserPermissionsQuery>
{
    public async Task<UserPermissionsDto> HandleAsync(GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserAsync(query.UserId, cancellationToken);

        return new(user);
    }
}