using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;

namespace SocialService.Friends.RemoveFriend;

public class RemoveFriendCommandHandler(IMongoContext mongoContext) : IHandler<bool, RemoveFriendCommand>
{
    public async Task<bool> HandleAsync(RemoveFriendCommand command, CancellationToken cancellationToken)
    {
        await mongoContext.RemoveFriendAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}