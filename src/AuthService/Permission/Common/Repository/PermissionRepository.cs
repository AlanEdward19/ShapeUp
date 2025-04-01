using AuthService.Common.User;
using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;

namespace AuthService.Permission.Common.Repository;

public class PermissionRepository(AuthDbContext dbContext) : IPermissionRepository
{
    public async Task AddAsync(Permission permission, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        await dbContext.Permissions.AddAsync(permission, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Permissions.Update(permission);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task DeleteAsync(Permission permission, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);

        dbContext.Permissions.Remove(permission);

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task<ICollection<Permission>> GetGroupPermissionsAsync(Guid groupId,
        CancellationToken cancellationToken)
    {
        Group.Group? group = await dbContext.Groups
            .Include(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        return group.GroupPermissions.Select(x => x.Permission).ToList();
    }

    public async Task<ICollection<Permission>> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        User? user = await dbContext.Users
            .Include(x => x.UserGroups)
            .ThenInclude(x => x.Group)
            .ThenInclude(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ObjectId == userId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        return user.UserGroups.SelectMany(x => x.Group.GroupPermissions.Select(x => x.Permission)).ToList();
    }

    public async Task GrantGroupPermissionAsync(Guid groupId, Guid permissionId, CancellationToken cancellationToken)
    {
        Group.Group? group = await dbContext.Groups
            .Include(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(x => x.Id == groupId, cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        Permission? permission = await dbContext.Permissions
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);

        if (permission is null)
            throw new NotFoundException($"Permission with id '{permissionId}' not found.");

        group.AddPermission(permission);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task GrantUserPermissionAsync(string userId, Guid permissionId, CancellationToken cancellationToken)
    {
        User? user = await dbContext.Users
            .Include(x => x.UserGroups)
            .ThenInclude(x => x.Group)
            .ThenInclude(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(x => x.ObjectId == userId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        Permission? permission = await dbContext.Permissions
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);

        if (permission is null)
            throw new NotFoundException($"Permission with id '{permissionId}' not found.");

        user.UserGroups.First(x => x.Role == EGroupRole.Owner).Group.AddPermission(permission);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Permission> GetPermissionAsync(Guid permissionId, CancellationToken cancellationToken)
    {
        Permission? permission = await dbContext.Permissions
            .FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);

        if (permission is null)
            throw new NotFoundException($"Permission with id '{permissionId}' not found.");

        return permission;
    }
}