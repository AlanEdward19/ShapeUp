using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.GrantUserPermission;

public class GrantUserPermissionCommandHandler(IPermissionRepository repository) : IHandler<bool, GrantUserPermissionCommand>
{
    public async Task<bool> HandleAsync(GrantUserPermissionCommand command, CancellationToken cancellationToken)
    {
        await repository.GrantUserPermissionAsync(command.UserId, command.PermissionId, cancellationToken);
        
        return true;
    }
}