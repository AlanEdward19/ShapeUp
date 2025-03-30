using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.DeleteGroup;

public class DeleteGroupCommandHandler(IGroupRepository repository) : IHandler<bool, DeleteGroupCommand>
{
    public async Task<bool> HandleAsync(DeleteGroupCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.GroupId, cancellationToken);
        
        return true;
    }
}