namespace AuthService.Permission.GetUserPermissions;

public class GetUserPermissionsQuery(string userId)
{
    public string UserId { get; private set; } = userId;
}