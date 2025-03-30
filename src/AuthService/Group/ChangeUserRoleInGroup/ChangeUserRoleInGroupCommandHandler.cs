using AuthService.Common.Interfaces;
using AuthService.Group.AddUserToGroup;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.ChangeUserRoleInGroup;

public class ChangeUserRoleInGroupCommandHandler(IGroupRepository repository) : IHandler<bool, ChangeUserRoleInGroupCommand>
{
    public async Task<bool> HandleAsync(ChangeUserRoleInGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.ChangeUserRoleInGroupAsync(command.GroupId, command.UserId, command.Role, cancellationToken);

        return true;
    }
}