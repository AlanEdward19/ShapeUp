namespace AuthService.Permission.GetGroupPermissions;

public class GetGroupPermissionsQuery(Guid groupId)
{
    public Guid GroupId { get; private set; } = groupId;
}