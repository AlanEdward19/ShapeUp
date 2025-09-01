using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;

namespace AuthService.Group.AddUserToGroup;

public class AddUserToGroupCommandHandler(IGroupRepository repository, IPermissionRepository permissionRepository) : IHandler<bool, AddUserToGroupCommand>
{
    public async Task<bool> HandleAsync(AddUserToGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.AddUserToGroupAsync(command.GroupId, command.UserId, command.Role, cancellationToken);
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