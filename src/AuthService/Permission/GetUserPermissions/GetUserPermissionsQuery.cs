namespace AuthService.Permission.GetUserPermissions;

public class GetUserPermissionsQuery(Guid userId)
{
    public Guid UserId { get; private set; } = userId;
}