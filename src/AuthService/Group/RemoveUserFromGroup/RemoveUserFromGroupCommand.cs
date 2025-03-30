namespace AuthService.Group.RemoveUserFromGroup;

public class RemoveUserFromGroupCommand(Guid groupId, Guid userId)
{
    public Guid GroupId { get; private set; } = groupId;
    public Guid UserId { get; private set; } = userId;
}