using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Group.Common.Repository;
using SharedKernel.Utils;

namespace AuthService.Group.CreateGroup;

public class CreateGroupCommandHandler(IGroupRepository repository) : IHandler<bool, CreateGroupCommand>
{
    public async Task<bool> HandleAsync(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        Group group = new();

        await repository.AddAsync(group, ProfileContext.ProfileId, cancellationToken);

        return true;
    }
}