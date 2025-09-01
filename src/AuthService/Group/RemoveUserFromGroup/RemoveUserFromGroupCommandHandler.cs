using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;

namespace AuthService.Group.RemoveUserFromGroup;

public class RemoveUserFromGroupCommandHandler(IGroupRepository repository, IPermissionRepository permissionRepository) : IHandler<bool, RemoveUserFromGroupCommand>
{
    public async Task<bool> HandleAsync(RemoveUserFromGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveUserFromGroupAsync(command.GroupId, command.UserId, cancellationToken);
        List<Permission.Permission> permissions =
            (await permissionRepository.GetUserPermissionsAsync(command.UserId, cancellationToken))
            .ToList();
        
        Dictionary<string, object> claims =
            (await FirebaseAuth.DefaultInstance.GetUserAsync(command.UserId, cancellationToken))
            .CustomClaims
            .ToDictionary();
        
        List<string> parsedScopes = [];

        foreach (Permission.Permission permission in permissions)
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