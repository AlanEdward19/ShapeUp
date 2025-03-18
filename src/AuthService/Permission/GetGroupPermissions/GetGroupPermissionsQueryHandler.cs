using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.GetGroupPermissions;

public class GetGroupPermissionsQueryHandler(IPermissionRepository repository) : IHandler<ICollection<PermissionDto>, GetGroupPermissionsQuery>
{
    public async Task<ICollection<PermissionDto>> HandleAsync(GetGroupPermissionsQuery query, CancellationToken cancellationToken)
    {
        ICollection<Permission> permissions = await repository.GetGroupPermissionsAsync(query.GroupId, cancellationToken);
        
        return permissions
            .Select(permission => new PermissionDto(permission))
            .ToList();
    }
}