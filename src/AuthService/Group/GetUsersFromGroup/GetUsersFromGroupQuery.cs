namespace AuthService.Group.GetUsersFromGroup;

public class GetUsersFromGroupQuery(Guid groupId)
{
    public Guid GroupId { get; private set; } = groupId;
}