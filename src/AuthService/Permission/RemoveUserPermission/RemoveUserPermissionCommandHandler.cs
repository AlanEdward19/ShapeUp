using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;
using AuthService.Permission.GrantUserPermission;
using FirebaseAdmin.Auth;
using Newtonsoft.Json.Linq;

namespace AuthService.Permission.RemoveUserPermission;

public class RemoveUserPermissionCommandHandler(IPermissionRepository repository)
    : IHandler<bool, RemoveUserPermissionCommand>
{
    public async Task<bool> HandleAsync(RemoveUserPermissionCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveUserPermissionAsync(command.UserId, command.PermissionId, cancellationToken);

        Dictionary<string, object> claims =
            (await FirebaseAuth.DefaultInstance.GetUserAsync(command.UserId, cancellationToken)).CustomClaims
            .ToDictionary();

        List<Permission> permissions =
            (await repository.GetUserPermissionsAsync(command.UserId, cancellationToken))
            .ToList();

        List<string> parsedScopes = [];

        foreach (Permission permission in permissions)
            parsedScopes.Add($"{permission.Action} - {permission.Theme}".ToLower());

        parsedScopes = parsedScopes
            .Distinct()
            .ToList();

        claims["scopes"] = parsedScopes;

        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(command.UserId, claims,
            cancellationToken);

        return true;
    }
}