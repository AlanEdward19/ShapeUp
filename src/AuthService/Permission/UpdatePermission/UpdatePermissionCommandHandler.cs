using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.UpdatePermission;

public class UpdatePermissionCommandHandler(IPermissionRepository repository) : IHandler<bool, UpdatePermissionCommand>
{
    public async Task<bool> HandleAsync(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission permission = await repository.GetPermissionAsync(command.PermissionId, cancellationToken);
        
        permission.SetAction(command.Action);
        permission.SetTheme(command.Theme);
        
        await repository.UpdateAsync(permission, cancellationToken);

        return true;
    }
}