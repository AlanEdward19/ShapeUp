using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.AddUserToGroup;

public class AddUserToGroupCommandHandler(IGroupRepository repository) : IHandler<bool, AddUserToGroupCommand>
{
    public async Task<bool> HandleAsync(AddUserToGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.AddUserToGroupAsync(command.GroupId, command.UserId, command.Role, cancellationToken);

        return true;
    }
}