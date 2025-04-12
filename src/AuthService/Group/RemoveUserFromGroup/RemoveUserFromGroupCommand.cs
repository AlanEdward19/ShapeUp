namespace AuthService.Group.RemoveUserFromGroup;

public class RemoveUserFromGroupCommand(Guid groupId, string userId)
{
    public Guid GroupId { get; private set; } = groupId;
    public string UserId { get; private set; } = userId;
}