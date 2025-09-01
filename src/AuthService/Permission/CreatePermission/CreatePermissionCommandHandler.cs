using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;

namespace AuthService.Permission.CreatePermission;

public class CreatePermissionCommandHandler(IPermissionRepository repository) : IHandler<PermissionDto, CreatePermissionCommand>
{
    public async Task<PermissionDto> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission permission = new(Guid.NewGuid(), command.Action, command.Theme, DateTime.Now, DateTime.Now);
        
        await repository.AddAsync(permission, cancellationToken);

        return new(permission);
    }
}