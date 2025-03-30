using SharedKernel.Enums;

namespace AuthService.Permission.UpdatePermission;

public class UpdatePermissionCommand(EPermissionAction? action, string? theme)
{
    public Guid PermissionId { get; private set; }
    public EPermissionAction? Action { get; private set; } = action;
    public string? Theme { get; private set; } = theme;
    
    public void SetPermissionId(Guid permissionId) => PermissionId = permissionId;
}