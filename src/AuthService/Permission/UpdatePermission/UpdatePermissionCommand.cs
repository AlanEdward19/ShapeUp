using AuthService.Permission.Common.Enums;

namespace AuthService.Permission.UpdatePermission;

public class UpdatePermissionCommand(Guid permissionId, EPermissionAction? action, string? theme)
{
    public Guid PermissionId { get; private set; } = permissionId;
    public EPermissionAction? Action { get; private set; } = action;
    public string? Theme { get; private set; } = theme;
}