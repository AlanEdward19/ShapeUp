using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;

namespace AuthService.Permission.UpdatePermission;

public class UpdatePermissionCommandHandler(IPermissionRepository repository) : IHandler<PermissionDto, UpdatePermissionCommand>
{
    public async Task<PermissionDto> HandleAsync(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission permission = await repository.GetPermissionAsync(command.PermissionId, cancellationToken);
        
        permission.SetAction(command.Action);
        permission.SetTheme(command.Theme);
        
        await repository.UpdateAsync(permission, cancellationToken);
        
        var users = await repository.GetUsersWithSpecificPermissionAsync(command.PermissionId, cancellationToken);
        
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

            foreach (Permission item in permissions)
                parsedScopes.Add($"{item.Action} - {item.Theme}".ToLower());

            parsedScopes = parsedScopes
                .Distinct()
                .ToList();

            claims["scopes"] = parsedScopes;

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(user.ObjectId, claims,
                cancellationToken);
        }

        return new(permission);
    }
}