using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionRepository repository) : IHandler<bool, CreatePermissionCommand>
{
    public async Task<bool> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission permission = new(command.Action, command.Theme);
        
        await repository.AddAsync(permission, cancellationToken);

        return true;
    }
}