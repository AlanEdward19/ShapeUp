using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;
using AuthService.Permission.Common.Repository;
using FirebaseAdmin.Auth;

namespace AuthService.Group.DeleteGroup;

public class DeleteGroupCommandHandler(IGroupRepository repository, IPermissionRepository permissionRepository) : IHandler<bool, DeleteGroupCommand>
{
    public async Task<bool> HandleAsync(DeleteGroupCommand command, CancellationToken cancellationToken)
    {
        var users = await repository.GetUsersFromGroupAsync(command.GroupId, cancellationToken);
        await repository.DeleteAsync(command.GroupId, cancellationToken);
        
        foreach (var user in users)
        {
            Dictionary<string, object> claims =
                (await FirebaseAuth.DefaultInstance.GetUserAsync(user.ObjectId, cancellationToken))
                .CustomClaims
                .ToDictionary();

            List<Permission.Permission> permissions =
                (await permissionRepository.GetUserPermissionsAsync(user.ObjectId, cancellationToken))
                .ToList();
            
            List<string> parsedScopes = [];

            foreach (Permission.Permission permission in permissions)
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