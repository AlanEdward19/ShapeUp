namespace AuthService.Permission.RemoveGroupPermission;

public class RemoveGroupPermissionCommand(Guid groupId, Guid permissionId)
{
    public Guid GroupId { get; private set; } = groupId;
    public Guid PermissionId { get; private set; } = permissionId;
}