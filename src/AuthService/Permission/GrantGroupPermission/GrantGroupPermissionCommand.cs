namespace AuthService.Permission.GrantGroupPermission;

public class GrantGroupPermissionCommand(Guid groupId, Guid permissionId)
{
    public Guid GroupId { get; private set; } = groupId;
    public Guid PermissionId { get; private set; } = permissionId;
}