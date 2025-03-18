namespace AuthService.Permission.DeletePermission;

public class DeletePermissionCommand(Guid permissionId)
{
    public Guid PermissionId { get; private set; } = permissionId;
}