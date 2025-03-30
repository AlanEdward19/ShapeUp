namespace AuthService.Permission.GrantUserPermission;

public class GrantUserPermissionCommand(Guid userId, Guid permissionId)
{
    public Guid UserId { get; private set; } = userId;
    public Guid PermissionId { get; private set; } = permissionId;
}