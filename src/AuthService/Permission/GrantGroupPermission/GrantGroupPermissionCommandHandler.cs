using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.GrantGroupPermission;

public class GrantGroupPermissionCommandHandler(IPermissionRepository repository) : IHandler<bool, GrantGroupPermissionCommand>
{
    public async Task<bool> HandleAsync(GrantGroupPermissionCommand command, CancellationToken cancellationToken)
    {
        await repository.GrantGroupPermissionAsync(command.GroupId, command.PermissionId, cancellationToken);
        
        return true;
    }
}