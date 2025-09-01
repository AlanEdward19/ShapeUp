namespace AuthService.Permission.GetUserPermissions;

public class GroupPermissionDto(Group.Group group)
{
    public Guid Id { get; set; } = group.Id;

    public ICollection<PermissionDto> Permissions { get; private set; } =
        group.GroupPermissions
            .Select(x => new PermissionDto(x.Permission)).ToList();
}