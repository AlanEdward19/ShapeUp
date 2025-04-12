namespace AuthService.Permission.RemoveUserPermission;

public class RemoveUserPermissionCommand(string userId, Guid permissionId)
{
    public string UserId { get; private set; } = userId;
    public Guid PermissionId { get; private set; } = permissionId;
}