using AuthService.Permission.Common.Enums;

namespace AuthService.Permission.CreatePermission;

public class CreatePermissionCommand(EPermissionAction action, string theme)
{
    public EPermissionAction Action { get; private set; } = action;
    public string Theme { get; private set; } = theme;
}