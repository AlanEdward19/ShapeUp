using AuthService.Group.Common.Enums;

namespace AuthService.Group.ChangeUserRoleInGroup;

public class ChangeUserRoleInGroupCommand(Guid groupId, string userId, EGroupRole role)
{
    public Guid GroupId { get; private set; } = groupId;
    public string UserId { get; private set; } = userId;
    public EGroupRole Role { get; private set; } = role;
}