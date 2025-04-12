namespace AuthService.Permission.GrantUserPermission;

public class GrantUserPermissionCommand(string userId, Guid permissionId)
{
    public string UserId { get; private set; } = userId;
    public Guid PermissionId { get; private set; } = permissionId;
}