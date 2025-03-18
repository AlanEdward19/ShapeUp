using AuthService.Group.Common.Enums;

namespace AuthService.Group.AddUserToGroup;

public class AddUserToGroupCommand(Guid groupId, Guid userId, EGroupRole role)
{
    public Guid GroupId { get; private set; } = groupId;
    public Guid UserId { get; private set; } = userId;
    public EGroupRole Role { get; private set; } = role;
}