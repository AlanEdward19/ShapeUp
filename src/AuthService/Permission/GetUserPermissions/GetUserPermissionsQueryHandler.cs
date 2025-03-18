using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.GetUserPermissions;

public class GetUserPermissionsQueryHandler(IPermissionRepository repository) : IHandler<ICollection<PermissionDto>, GetUserPermissionsQuery>
{
    public async Task<ICollection<PermissionDto>> HandleAsync(GetUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        ICollection<Permission> permissions = await repository.GetUserPermissionsAsync(query.UserId, cancellationToken);
        
        return permissions
            .Select(permission => new PermissionDto(permission))
            .ToList();
    }
}