using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.RemoveUserFromGroup;

public class RemoveUserFromGroupCommandHandler(IGroupRepository repository) : IHandler<bool, RemoveUserFromGroupCommand>
{
    public async Task<bool> HandleAsync(RemoveUserFromGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveUserFromGroupAsync(command.GroupId, command.UserId, cancellationToken);
        
        return true;
    }
}