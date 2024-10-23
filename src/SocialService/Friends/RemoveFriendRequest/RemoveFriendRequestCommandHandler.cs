using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;

namespace SocialService.Friends.RemoveFriendRequest;

public class RemoveFriendRequestCommandHandler(IMongoContext mongoContext) : IHandler<bool, RemoveFriendRequestCommand>
{
    public async Task<bool> HandleAsync(RemoveFriendRequestCommand command, CancellationToken cancellationToken)
    {
        await mongoContext.RemoveRequestFromProfile(ProfileContext.ProfileId, command.ProfileId, true);
        await mongoContext.RemoveRequestFromProfile(command.ProfileId, ProfileContext.ProfileId, false);
        
        return true;
    }
}