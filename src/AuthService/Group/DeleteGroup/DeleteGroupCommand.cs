namespace AuthService.Group.DeleteGroup;

public class DeleteGroupCommand(Guid groupId)
{
    public Guid GroupId { get; private set; } = groupId;
}