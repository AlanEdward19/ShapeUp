using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;
using Newtonsoft.Json.Linq;

namespace AuthService.Permission.GrantUserPermission;

public class GrantUserPermissionCommandHandler(IPermissionRepository repository)
    : IHandler<bool, GrantUserPermissionCommand>
{
    public async Task<bool> HandleAsync(GrantUserPermissionCommand command, CancellationToken cancellationToken)
    {
        await repository.GrantUserPermissionAsync(command.UserId, command.PermissionId, cancellationToken);

        Dictionary<string, object> claims =
            (await FirebaseAuth.DefaultInstance.GetUserAsync(command.UserId, cancellationToken)).CustomClaims
            .ToDictionary();

        List<Permission> permissions =
            (await repository.GetUserPermissionsAsync(command.UserId, cancellationToken))
            .ToList();

        claims.TryGetValue("scopes", out object? scopes);

        List<string> parsedScopes =
            scopes != null ? (scopes as JArray)!.ToObject<List<string>>()! : [];

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