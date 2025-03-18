using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.DeletePermission;

public class DeletePermissionCommandHandler(IPermissionRepository repository) : IHandler<bool, DeletePermissionCommand>
{
    public async Task<bool> HandleAsync(DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission permission = await repository.GetPermissionAsync(command.PermissionId, cancellationToken);
        
        await repository.DeleteAsync(permission, cancellationToken);
        
        return true;
    }
}