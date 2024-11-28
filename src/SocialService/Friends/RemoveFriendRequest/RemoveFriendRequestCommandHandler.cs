using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.RemoveFriendRequest;

public class RemoveFriendRequestCommandHandler(IFriendshipGraphRepository graphRepository) : IHandler<bool, RemoveFriendRequestCommand>
{
    public async Task<bool> HandleAsync(RemoveFriendRequestCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.RejectFriendRequestAsync(ProfileContext.ProfileId, command.ProfileId);
        
        return true;
    }
}