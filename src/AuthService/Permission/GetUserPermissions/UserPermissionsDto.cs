using AuthService.Common.User;
using AuthService.Group.Common.Enums;

namespace AuthService.Permission.GetUserPermissions;

public class UserPermissionsDto
{
    public ICollection<PermissionDto> UserPermissions { get; private set; }
    public ICollection<GroupPermissionDto> GroupPermissions { get; private set; }

    public UserPermissionsDto(User user)
    {
        var userPermissions = user.UserGroups.Where(x => x.Role == EGroupRole.Owner)
            .Select(x => x.Group)
            .Select(x => x.GroupPermissions)
            .SelectMany(x => x.Select(gp => new PermissionDto(gp.Permission)));
        UserPermissions = userPermissions.ToList();
        
        var groupPermissions = user.UserGroups.Where(x => x.Role != EGroupRole.Owner)
            .Select(x => new GroupPermissionDto(x.Group));
        GroupPermissions = groupPermissions.ToList();
    }
}