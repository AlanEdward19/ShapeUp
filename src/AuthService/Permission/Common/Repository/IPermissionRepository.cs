namespace AuthService.Permission.Common.Repository;

public interface IPermissionRepository
{
    Task AddAsync(Permission permission, CancellationToken cancellationToken);
    Task UpdateAsync(Permission permission, CancellationToken cancellationToken);
    Task DeleteAsync(Permission permission, CancellationToken cancellationToken);
    
    Task<ICollection<Permission>> GetGroupPermissionsAsync(Guid groupId,
        CancellationToken cancellationToken);
    Task<ICollection<Permission>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Permission> GetPermissionAsync(Guid permissionId, CancellationToken cancellationToken);
    
    Task GrantGroupPermissionAsync(Guid groupId, Guid permissionId, CancellationToken cancellationToken);
    Task GrantUserPermissionAsync(Guid userId, Guid permissionId, CancellationToken cancellationToken);
}