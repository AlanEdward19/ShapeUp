using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;

namespace AuthService.Permission.RemoveGroupPermission;

public class RemoveGroupPermissionCommandHandler(IPermissionRepository repository, IGroupRepository groupRepository)
    : IHandler<bool, RemoveGroupPermissionCommand>
{
    public async Task<bool> HandleAsync(RemoveGroupPermissionCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveGroupPermissionAsync(command.GroupId, command.PermissionId, cancellationToken);

        var users = await groupRepository.GetUsersFromGroupAsync(command.GroupId, cancellationToken);

        foreach (var user in users)
        {
            Dictionary<string, object> claims =
                (await FirebaseAuth.DefaultInstance.GetUserAsync(user.ObjectId, cancellationToken))
                .CustomClaims
                .ToDictionary();

            List<Permission> permissions =
                (await repository.GetUserPermissionsAsync(user.ObjectId, cancellationToken))
                .ToList();
            
            List<string> parsedScopes = [];

            foreach (Permission permission in permissions)
                parsedScopes.Add($"{permission.Action} - {permission.Theme}".ToLower());

            parsedScopes = parsedScopes
                .Distinct()
                .ToList();

            claims["scopes"] = parsedScopes;

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(user.ObjectId, claims,
                cancellationToken);
        }

        return true;
    }
}