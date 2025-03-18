using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;

namespace AuthService.Group.CreateGroup;

public class CreateGroupCommandHandler(IGroupRepository repository) : IHandler<bool, CreateGroupCommand>
{
    public async Task<bool> HandleAsync(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        Group group = new();

        await repository.AddAsync(group, cancellationToken);
        
        return true;
    }
}